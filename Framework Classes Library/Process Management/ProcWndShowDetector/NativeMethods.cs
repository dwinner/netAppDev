using System;
using System.Runtime.InteropServices;

namespace ProcWndShowDetector
{
   /// <summary>
   ///     "Родные" методы
   /// </summary>
   internal static class NativeMethods
   {
      /// <summary>
      ///     Определяет, является ли окно видимым
      /// </summary>
      /// <param name="hWnd">Значение оконного дескриптора</param>
      /// <returns>true, если окно видимо, false - в противном случае</returns>
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      internal static extern bool IsWindowVisible(IntPtr hWnd);
   }
}