/**
 * Чтение списков ACL для файла
 */

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ReadingFileAcl
{
   class Program
   {
      private static string _filePath;

      static void Main()
      {
         Console.Write("Provide full file path: ");
         _filePath = Console.ReadLine();

         try
         {
            using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
               var fileSecurity = fileStream.GetAccessControl();
               foreach (FileSystemAccessRule fileSystemAccessRule in fileSecurity.GetAccessRules(true, true, typeof(NTAccount)))
               {
                  Console.WriteLine("{0} {1} {2} access for {3}",
                     _filePath,
                     fileSystemAccessRule.AccessControlType == AccessControlType.Allow ? "provides" : "denies",
                     fileSystemAccessRule.FileSystemRights,
                     fileSystemAccessRule.IdentityReference);
               }
            }
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }
      }
   }
}
