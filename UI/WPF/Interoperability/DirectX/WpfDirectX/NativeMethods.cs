using System;
using System.Runtime.InteropServices;

namespace WpfDirectX
{
   internal static class NativeMethods
   {
      private const string DirectXDll = "DirectXSample.dll";

      [DllImport(DirectXDll)]
      internal static extern IntPtr Initialize(IntPtr hwnd, int width, int height);

      [DllImport(DirectXDll)]
      internal static extern void Render();

      [DllImport(DirectXDll)]
      internal static extern void Cleanup();
   }
}