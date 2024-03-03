using System.Runtime.InteropServices;

namespace UnmanagedCallbackFun;

internal static class Program
{
   private static readonly EnumWindowsCallback PrintWindowFunc = PrintWindow;

   private static void Main()
   {
      EnumWindows(PrintWindowFunc, IntPtr.Zero);
   }

   [DllImport("user32.dll")]
   private static extern int EnumWindows(EnumWindowsCallback hWnd, IntPtr lParam);

   private static bool PrintWindow(IntPtr hWnd, IntPtr lParam)
   {
      Console.WriteLine(hWnd.ToInt64());
      return true;
   }

   private delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam);
}