using System;
using System.Runtime.InteropServices;

namespace EventLogDemo
{
   class Win32
   {
      [DllImport("User32.dll")]
      public static extern IntPtr SendMessage(HandleRef hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

      // Определена в CommCtrl.h
      public const UInt32 BcmSetshield = 0x0000160C;
   }
}
