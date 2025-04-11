using System;
using System.Runtime.InteropServices;

namespace Killing
{
   public static class Unmanaged
   {
      [DllImport("kernel32.dll", SetLastError = true)]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
   }
}