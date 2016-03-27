using System;
using System.Runtime.InteropServices;

namespace TransparentForm
{
   internal static class Win32
   {
      public const int WmNclbuttondown = 0xA1;
      public const int Htcaption = 0x2;

      [DllImport("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
   }
}