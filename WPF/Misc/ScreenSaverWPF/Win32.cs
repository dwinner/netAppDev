using System;
using System.Runtime.InteropServices;

namespace ScreenSaverWPF
{
   internal static class Win32
   {
      [DllImport("user32.dll")]
      public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

      [Serializable]
      [StructLayout(LayoutKind.Sequential)]
      public struct Rect
      {
         public readonly int Left;
         public readonly int Top;
         private readonly int Right;
         private readonly int Bottom;

         public Rect(int left, int top, int right, int bottom)
         {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
         }

         public int Height
         {
            get { return Bottom - Top; }
         }

         public int Width
         {
            get { return Right - Left; }
         }
      }
   }
}