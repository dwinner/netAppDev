using System;
using System.Runtime.Versioning;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace FileSystemTransactions
{
   [SecurityCritical]
   internal sealed class SafeTransactionHandle : SafeHandleZeroOrMinusOneIsInvalid
   {
      public SafeTransactionHandle(IntPtr preexistingHandle, bool ownsHandle)
         : base(ownsHandle)
      {
         SetHandle(preexistingHandle);
      }

      [ResourceExposure(ResourceScope.Machine)]
      [ResourceConsumption(ResourceScope.Machine)]
      protected override bool ReleaseHandle()
      {
         return NativeMethods.CloseHandle(handle);
      }
   }
}
