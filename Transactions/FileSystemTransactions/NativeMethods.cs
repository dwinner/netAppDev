using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace FileSystemTransactions
{
   [SecurityCritical]
   internal static class NativeMethods
   {
      [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
      internal static extern SafeFileHandle CreateFileTransacted(
          string lpFileName,
          uint dwDesiredAccess,
          uint dwShareMode,
          IntPtr lpSecurityAttributes,
          uint dwCreationDisposition,
          int dwFlagsAndAttributes,
          IntPtr hTemplateFile,
          SafeTransactionHandle txHandle,
          IntPtr miniVersion,
          IntPtr extendedParameter);

      [DllImport("Kernel32.dll", SetLastError = true)]
      [ResourceExposure(ResourceScope.Machine)]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool CloseHandle(IntPtr handle);
   }
}

