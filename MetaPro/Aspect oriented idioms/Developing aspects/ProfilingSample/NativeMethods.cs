namespace ProfilingSample
{
   using System;
   using System.Runtime.InteropServices;
   using System.Security;

   internal static class NativeMethods
   {
      [DllImport("kernel32.dll", SetLastError = true)]
      [SuppressUnmanagedCodeSecurity]
      internal static extern bool GetThreadTimes(IntPtr threadHandle, out ulong creationTime, out ulong exitTime,
                                               out ulong kernelTime, out ulong userTime);

      [DllImport("kernel32.dll")]
      [SuppressUnmanagedCodeSecurity]
      internal static extern IntPtr GetCurrentThread();
   }
}