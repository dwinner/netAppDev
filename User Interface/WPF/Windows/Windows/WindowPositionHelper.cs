using System.Globalization;
using System.Windows;
using Microsoft.Win32;

namespace Windows
{
   public static class WindowPositionHelper
   {
      private const string RegPath = @"Software\MyApp\";
      private const string WindowsBoundsLabel = "Bounds";

      public static void SaveSize(Window win)
      {
         // Create or retrieve a reference to a key where the settings
         // will be stored.
         var key = Registry.CurrentUser.CreateSubKey(string.Format("{0}{1}", RegPath, win.Name));
         if (key != null)
            key.SetValue(WindowsBoundsLabel, win.RestoreBounds.ToString(CultureInfo.InvariantCulture));
      }

      public static void SetSize(Window win)
      {
         var key = Registry.CurrentUser.OpenSubKey(string.Format("{0}{1}", RegPath, win.Name));
         if (key != null)
         {
            var bounds = Rect.Parse(key.GetValue(WindowsBoundsLabel).ToString());

            win.Top = bounds.Top;
            win.Left = bounds.Left;

            // Only restore the size for a manually sized
            // window.
            if (win.SizeToContent == SizeToContent.Manual)
            {
               win.Width = bounds.Width;
               win.Height = bounds.Height;
            }
         }
      }
   }
}