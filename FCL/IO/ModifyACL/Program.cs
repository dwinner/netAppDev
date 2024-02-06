using System.Security.AccessControl;
using System.Security.Principal;

var file = "sectest.txt";
File.WriteAllText(file, "File security.");

var sid = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
var usersAccount = sid.Translate(typeof(NTAccount)).ToString();

Console.WriteLine($"User: {usersAccount}");

var sec = new FileSecurity(file,
   AccessControlSections.Owner |
   AccessControlSections.Group |
   AccessControlSections.Access);

Console.WriteLine("AFTER CREATE:");
ShowSecurity(sec);

sec.ModifyAccessRule(AccessControlModification.Add,
   new FileSystemAccessRule(usersAccount, FileSystemRights.Write,
      AccessControlType.Allow),
   out var modified);

Console.WriteLine("AFTER MODIFY:");
ShowSecurity(sec);

return;

void ShowSecurity(FileSecurity sec)
{
   var rules = sec.GetAccessRules(true, true,
      typeof(NTAccount));
   foreach (var r in rules.Cast<FileSystemAccessRule>()
               .OrderBy(rule => rule.IdentityReference.Value))
   {
      // e.g., MyDomain/Joe
      Console.WriteLine($"  {r.IdentityReference.Value}");
      // Allow or Deny: e.g., FullControl
      Console.WriteLine($"    {r.FileSystemRights}: {r.AccessControlType}");
   }
}