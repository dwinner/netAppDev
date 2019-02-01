/**
 * Библиотека для вызова из Sandbox API
 */

using System;
using System.IO;
using System.Security;

[assembly: AllowPartiallyTrustedCallers]

namespace RequireFileIOPermissionsDemo
{
   [SecuritySafeCritical]
   public class RequirePermissionsDemo : MarshalByRefObject
   {
      public bool RequireFilePermissions(string path)
      {
         bool accessAllowed = true;

         try
         {
            StreamWriter writer = File.CreateText(path);
            writer.WriteLine("written successfully");
            writer.Close();
         }
         catch (SecurityException)
         {
            accessAllowed = false;
         }

         return accessAllowed;
      }
   }
}
