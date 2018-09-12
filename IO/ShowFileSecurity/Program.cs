/**
 * Получение информации, связанной с безопасностью
 */

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ShowFileSecurity
{
   class Program
   {
      private const string DefaultFileName = "ReSharper_Hot_Keys.txt";

      static void Main(string[] args)
      {
         string filename = args.Length < 1 ? DefaultFileName : args[0];
         FileInfo info = new FileInfo(filename);
         FileSecurity security = info.GetAccessControl();
         ShowSecurity(security);

         Console.ReadKey();
      }

      private static void ShowSecurity(CommonObjectSecurity security)
      {
         AuthorizationRuleCollection coll = security.GetAccessRules(true, true, typeof(NTAccount));
         foreach (FileSystemAccessRule rule in coll)
         {
            Console.WriteLine("IdentityReference: {0}", rule.IdentityReference);
            Console.WriteLine("Access control type: {0}", rule.AccessControlType);
            Console.WriteLine("Rights: {0}", rule.FileSystemRights);
            Console.WriteLine("Inherited? {0}", rule.IsInherited);

            Console.WriteLine();
         }
      }
   }
}
