using System;
using System.IO;
using static ResourceHolder.Unmanaged;

namespace ResourceHolder
{
   public sealed class DisposeRoutineImpl : DisposeRoutine
   {
      private readonly Stream _managedRes;
      private readonly IntPtr _unmanagedRes;

      public DisposeRoutineImpl(Stream aRes, string aFileName)
      {
         _managedRes = aRes;
         _unmanagedRes = CreateFile(aFileName, 0x80000000, 1, IntPtr.Zero, 3, 0, IntPtr.Zero);
      }

      protected override void UnmanagedCleaning()
      {
         if (_unmanagedRes != IntPtr.Zero)
         {
            CloseHandle(_unmanagedRes);
         }
      }

      protected override void ManagedCleaning() => _managedRes.Dispose();
   }
}