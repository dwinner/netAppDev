// Требование разрешений программным образом

using System.Security;
using System.Security.Permissions;

[assembly: AllowPartiallyTrustedCallers]


namespace DemandPermissionDemo
{
   [SecuritySafeCritical]
   public class DemandPermissions
   {
      public void DemandFileIoPermissions(string path)
      {
         var fileIoPermission = new FileIOPermission(PermissionState.Unrestricted);
         fileIoPermission.Demand();

         //...
      }
   }
}
