using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _03_ImageProcessorPipeline.Infrastructure
{
   public static class BitmapImageExtensions
   {
      public static BitmapSource ToGrayScale(this BitmapImage @this)
      {
         var img = @this;

         var width = (int) img.Width;
         var height = (int) img.Height;
         var dpi = img.DpiX;

         var pixelData = new byte[(int) img.Width * (int) img.Height * 4];
         var outputData = new byte[width * height];

         img.CopyPixels(pixelData, (int) img.Width * 4, 0);

         //var totalIterations = 5 * width;

         for (var i = 0; i < 1; i++)
         for (var x = 0; x < img.Width; x++)
         for (var y = 0; y < img.Height; y++)
         {
            var red = pixelData[y * (int) img.Width * 4 + x * 4 + 1];
            var green = pixelData[y * (int) img.Width * 4 + x * 4 + 2];
            var blue = pixelData[y * (int) img.Width * 4 + x * 4 + 3];

            var gray = (byte) (red * 0.299 + green * 0.587 + blue * 0.114);

            outputData[y * width + x] = gray;
         }

         var grey = BitmapSource.Create(width, height, dpi, dpi, PixelFormats.Gray8, null, outputData, width);
         grey.Freeze();
         return grey;
      }
   }
}