using System.Security.AccessControl;
using System.Security.Principal;
using static System.Security.AccessControl.AccessControlSections;

try
{
   File.WriteAllText("sectest.txt", "File for testing security.");
   var fSecurity = new FileSecurity("sectest.txt",
      Owner | Group | Access);

   Console.WriteLine(string.Join(Environment.NewLine,
      fSecurity.GetAccessRules(true, true, typeof(NTAccount))
         .Cast<AuthorizationRule>()
         .Select(r => r.IdentityReference.Value)));
}
catch (PlatformNotSupportedException ex)
{
   Console.WriteLine($"Not supported: {ex.Message}");
}
finally
{
   File.Delete("sectest.txt");
}