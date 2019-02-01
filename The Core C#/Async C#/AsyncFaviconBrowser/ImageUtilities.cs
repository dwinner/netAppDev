using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FaviconBrowser
{
   public static class ImageUtilities
   {
      public static Image MakeImageControl(byte[] bytes, int imageWidth = 16)
      {
         Image imageControl = new Image();
         var bitmapImage = MakeBitmapImage(bytes);
         imageControl.Source = bitmapImage;
         imageControl.Width = imageWidth;
         imageControl.Height = imageWidth;
         return imageControl;
      }

      public static BitmapImage MakeBitmapImage(byte[] bytes)
      {
         BitmapImage bitmapImage = new BitmapImage();
         bitmapImage.BeginInit();
         bitmapImage.StreamSource = new MemoryStream(bytes);
         bitmapImage.EndInit();
         return bitmapImage;
      }
   }
}
