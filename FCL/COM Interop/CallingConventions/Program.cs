using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CallingConventions;

internal static class Program
{
   private static unsafe void Main()
   {
      EnumWindows(&PrintWindow, IntPtr.Zero);
   }

   [DllImport("user32.dll")]
   private static extern unsafe int EnumWindows(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte> hWnd, IntPtr lParam);

   [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
   private static byte PrintWindow(IntPtr hWnd, IntPtr lParam)
   {
      Console.WriteLine(hWnd.ToInt64());
      return 1;
   }
}