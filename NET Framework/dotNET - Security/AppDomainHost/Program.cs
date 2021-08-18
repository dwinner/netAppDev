/**
 * Запуск кода в "песочнице"
 */

using System;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using RequireFileIOPermissionsDemo;

namespace AppDomainHost
{
   class Program
   {
      static void Main()
      {
         var permissionSet = new PermissionSet(PermissionState.None);
         permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
         // NOTE: если выдать разрешение permSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, "c:/temp"));
         // будет работать

         AppDomainSetup setup = AppDomain.CurrentDomain.SetupInformation;

         AppDomain newDomain = AppDomain.CreateDomain(
             "Sandboxed domain", AppDomain.CurrentDomain.Evidence, setup, permissionSet);
         ObjectHandle oh = newDomain.CreateInstance("RequireFileIOPermissionsDemo",
            "RequireFileIOPermissionsDemo.RequirePermissionsDemo");
         object unwrapObject = oh.Unwrap();
         var io = unwrapObject as RequirePermissionsDemo;
         const string path = @"c:\temp\file.txt";
         Console.WriteLine("has {0}permissions to write to {1}",
            io != null && io.RequireFilePermissions(path) ? null : "no ", path);
      }
   }
}
