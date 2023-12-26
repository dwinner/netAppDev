/**
 * Неявные разрешения
 */

using System;
using System.Security;
using System.Security.Permissions;

namespace ImplicitPermissions
{
   class Program
   {
      static void Main()
      {
         CodeAccessPermission permissionA =
            new FileIOPermission(FileIOPermissionAccess.AllAccess, @"C:\");
         CodeAccessPermission permissionB =
            new FileIOPermission(FileIOPermissionAccess.Read, @"C:\temp");
         if (permissionB.IsSubsetOf(permissionA))
         {
            Console.WriteLine("PermissionB is a subset of PermissionA");
         }
      }
   }
}
