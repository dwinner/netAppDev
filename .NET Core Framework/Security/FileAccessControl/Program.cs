/**
 * ACL на примере доступа к файлу
 */

using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;

namespace FileAccessControl
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length == 0)
         {
            return;
         }
         string fileName = args[0];

         ReadAccessRules(fileName);
      }

      private static void ReadAccessRules(string fileName)
      {
         using (FileStream stream = File.Open(fileName, FileMode.Open))
         {
            FileSecurity securityDescriptor = stream.GetAccessControl();
            var rules = securityDescriptor.GetAccessRules(true, true, typeof(NTAccount));
            foreach (var fileRule in rules.OfType<FileSystemAccessRule>())
            {
               Console.WriteLine("Access type: {0}", fileRule.AccessControlType);
               Console.WriteLine("Rights: {0}", fileRule.FileSystemRights);
               Console.WriteLine("Identity: {0}", fileRule.IdentityReference.Value);
            }
         }
      }

      private static void WriteAccessRules(string fileName)
      {
         var salesAccount = new NTAccount("Sales");
         var developersAccount = new NTAccount("Developers");
         var everyoneIdentity = new NTAccount("Everyone");

         var salesAccessRule = new FileSystemAccessRule(salesAccount, FileSystemRights.Write,
            AccessControlType.Deny);
         var everyoneAccessRule = new FileSystemAccessRule(everyoneIdentity, FileSystemRights.Read,
            AccessControlType.Allow);
         var developersAccessRule = new FileSystemAccessRule(developersAccount, FileSystemRights.FullControl,
            AccessControlType.Allow);

         var securityDescriptor = new FileSecurity();
         securityDescriptor.SetAccessRule(everyoneAccessRule);
         securityDescriptor.SetAccessRule(developersAccessRule);
         securityDescriptor.SetAccessRule(salesAccessRule);

         File.SetAccessControl(fileName, securityDescriptor);
      }
   }
}
