using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace _09_CriticalFinalizers
{
   //
   // Безопасный обработчик перечисления устройства Bluetooth
   //
   [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
   internal sealed class SafeBluetoothRadioFindHandle
      : SafeHandleZeroOrMinusOneIsInvalid
   {
      public SafeBluetoothRadioFindHandle()
         : base(true)
      {
      }

      protected override bool ReleaseHandle()
      {
         return BluetoothFindRadioClose(handle);
      }

      [DllImport("Irprops.cpl")]
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
      [SuppressUnmanagedCodeSecurity]
      private static extern bool BluetoothFindRadioClose(IntPtr hFind);
   }
}