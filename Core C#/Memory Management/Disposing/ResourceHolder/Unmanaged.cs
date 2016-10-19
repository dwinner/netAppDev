using System;
using System.Runtime.InteropServices;

namespace ResourceHolder
{
   public static class Unmanaged
   {
      [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
         SetLastError = true)]
      public static extern IntPtr CreateFile(
         string lpFileName,
         uint dwDesiredAccess,
         uint dwSharedMode,
         IntPtr securityAttributes,
         uint dwCreationDisposition,
         uint dwFlagsAndAttributes,
         IntPtr hTemplateFile);

      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool CloseHandle(IntPtr hObject);
   }
}