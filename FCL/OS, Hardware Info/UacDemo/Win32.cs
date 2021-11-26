using System;
using System.Runtime.InteropServices;

namespace EventLogDemo
{
   internal static class Win32
   {
      // Определена в CommCtrl.h
      public const uint BcmSetshield = 0x0000160C;

      [DllImport("User32.dll")]
      public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);
   }
}