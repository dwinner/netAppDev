using System.Runtime.InteropServices;

unsafe
{
   EnumWindows(&PrintWindow, IntPtr.Zero);
   return;

   [DllImport("user32.dll")]
   static extern int EnumWindows(delegate*<IntPtr, IntPtr, bool> hWnd, IntPtr lParam);

   static bool PrintWindow(IntPtr hWnd, IntPtr lParam)
   {
      Console.WriteLine(hWnd.ToInt64());
      return true;
   }
}