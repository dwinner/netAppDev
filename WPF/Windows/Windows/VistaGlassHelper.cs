using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Windows
{
   public static class VistaGlassHelper
   {
      private static Margins GetDpiAdjustedMargins(IntPtr windowHandle, int left, int right, int top)
      {
         // Get System Dpi
         var desktop = Graphics.FromHwnd(windowHandle);
         var desktopDpiX = desktop.DpiX;
         //var desktopDpiY = desktop.DpiY;

         // Note that the default desktop Dpi is 96dpi. The  margins are
         // adjusted for the system Dpi.

         // Set Margins
         var margins = new Margins
         {
            cxLeftWidth = Convert.ToInt32(left * (desktopDpiX / 96)),
            cxRightWidth = Convert.ToInt32(right * (desktopDpiX / 96)),
            cyTopHeight = Convert.ToInt32(top * (desktopDpiX / 96)),
            cyBottomHeight = Convert.ToInt32(right * (desktopDpiX / 96))
         };         

         return margins;
      }

      [DllImport("DwmApi.dll")]
      private static extern int DwmExtendFrameIntoClientArea(
         IntPtr hwnd,
         ref Margins pMarInset);

      public static void ExtendGlass(Window win, int left, int right, int top)
      {
         // Obtain the window handle for WPF application
         var windowInterop = new WindowInteropHelper(win);
         var windowHandle = windowInterop.Handle;
         var mainWindowSrc = HwndSource.FromHwnd(windowHandle);
         if (mainWindowSrc != null)
         {
            if (mainWindowSrc.CompositionTarget != null)
               mainWindowSrc.CompositionTarget.BackgroundColor = Colors.Transparent;

            var margins =
               GetDpiAdjustedMargins(windowHandle, left, right, top);

            var returnVal = DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
            if (returnVal < 0)
               throw new NotSupportedException("Operation failed.");
         }
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct Margins
      {
         public int cxLeftWidth; // width of left border that retains its size
         public int cxRightWidth; // width of right border that retains its size
         public int cyTopHeight; // height of top border that retains its size
         public int cyBottomHeight; // height of bottom border that retains its size
      }
   }
}