using System;
using System.Runtime.InteropServices;

namespace ProcWndShowDetector
{
   internal static class NativeMethods
   {
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool IsWindowVisible(IntPtr hWnd);
   }
}