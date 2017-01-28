using System.Runtime.InteropServices;

namespace ToastNotifications.ComDecl
{
   [ComImport]
   [Guid("0000010b-0000-0000-C000-000000000046")]
   [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
   internal interface IPersistFile
   {
      string GetCurFile();

      [PreserveSig]
      uint IsDirty();

      void Load(string pszFileName, long dwMode);
      void Save(string pszFileName, bool fRemember);
      void SaveCompleted(string pszFileName);
   }
}