using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace SingleInstanceApplication
{
   public static class FileRegistrationHelper
   {
      public static void SetFileAssociation(string extension, string progId)
      {
         // Create extension subkey
         SetValue(Registry.ClassesRoot, extension, progId);

         // Create progid subkey
         var assemblyFullPath = Assembly.GetExecutingAssembly().Location.Replace("/", @"\");
         var sbShellEntry = new StringBuilder();
         sbShellEntry.AppendFormat("\"{0}\" \"%1\"", assemblyFullPath);
         SetValue(Registry.ClassesRoot, progId + @"\shell\open\command", sbShellEntry.ToString());
         var sbDefaultIconEntry = new StringBuilder();
         sbDefaultIconEntry.AppendFormat("\"{0}\",0", assemblyFullPath);
         SetValue(Registry.ClassesRoot, progId + @"\DefaultIcon", sbDefaultIconEntry.ToString());

         // Create application subkey
         SetValue(Registry.ClassesRoot, @"Applications\" + Path.GetFileName(assemblyFullPath), "", "NoOpenWith");
      }

      private static void SetValue(RegistryKey root, string subKey, object keyValue, string valueName = null)
      {
         var hasSubKey = !string.IsNullOrEmpty(subKey);
         var key = root;

         try
         {
            if (hasSubKey)
               key = root.CreateSubKey(subKey);
            if (key != null)
               key.SetValue(valueName, keyValue);
         }
         finally
         {
            if (hasSubKey && (key != null))
               key.Close();
         }
      }
   }
}