/**
 * Чтение списков ACL для каталога
 */

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ReadingACLsFromDirectory
{
   internal class Program
   {
      private static string _mentionedDir;

      private static void Main()
      {
         Console.Write("Provide full directory path: ");
         _mentionedDir = Console.ReadLine();

         try
         {
            if (_mentionedDir != null)
            {
               var directoryInfo = new DirectoryInfo(_mentionedDir);

               if (directoryInfo.Exists)
               {
                  DirectorySecurity myDirSec = directoryInfo.GetAccessControl();

                  foreach (FileSystemAccessRule fileRule in myDirSec.GetAccessRules(true, true, typeof(NTAccount)))
                  {
                     Console.WriteLine("{0} {1} {2} access for {3}",
                        _mentionedDir,
                        fileRule.AccessControlType == AccessControlType.Allow ? "provides" : "denies",
                        fileRule.FileSystemRights,
                        fileRule.IdentityReference);
                  }
               }
            }
         }
         catch
         {
            Console.WriteLine("Incorrect directory provided!");
         }

         Console.ReadLine();
      }
   }
}
