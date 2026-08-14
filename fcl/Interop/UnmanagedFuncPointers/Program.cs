using System.Runtime.InteropServices;

namespace UnmanagedFuncPointers;

internal static class Program
{
   private static unsafe void Main()
   {
      EnumWindows(&PrintWindow, IntPtr.Zero);
   }

   [DllImport("user32.dll")]
   private static extern unsafe int EnumWindows(delegate* unmanaged<IntPtr, IntPtr, byte> hWnd, IntPtr lParam);

   [UnmanagedCallersOnly]
   private static byte PrintWindow(IntPtr hWnd, IntPtr lParam)
   {
      Console.WriteLine(hWnd.ToInt64());
      return 1;
   }
}