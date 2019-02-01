using System.Windows;
using Windows.Properties;

namespace Windows
{
   public sealed class WindowPositionHelperConfig
   {
      public static void SaveSize(Window win)
      {
         Settings.Default.WindowPosition = win.RestoreBounds;
         Settings.Default.Save();
      }

      public static void SetSize(Window win)
      {
         var rect = Settings.Default.WindowPosition;
         win.Top = rect.Top;
         win.Left = rect.Left;

         // Only restore the size for a manually sized
         // window.
         if (win.SizeToContent == SizeToContent.Manual)
         {
            win.Width = rect.Width;
            win.Height = rect.Height;
         }
      }
   }
}