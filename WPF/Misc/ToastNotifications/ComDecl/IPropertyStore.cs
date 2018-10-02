using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MS.WindowsAPICodePack.Internal;

namespace ToastNotifications.ComDecl
{
   [ComImport]
   [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
   [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
   internal interface IPropertyStore
   {
      uint GetCount();
      PropertyKey GetAt(uint propertyIndex);
      PropVariant GetValue([In] ref PropertyKey key);
      void SetValue([In] ref PropertyKey key, PropVariant pv);
      void Commit();
   }
}