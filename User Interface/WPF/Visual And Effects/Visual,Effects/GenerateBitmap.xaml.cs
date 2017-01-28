using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Drawing
{
   public partial class GenerateBitmap
   {
      public GenerateBitmap()
      {
         InitializeComponent();
      }

      private void OnGenerateBitmap(object sender, RoutedEventArgs e)
      {
         // Create the bitmap, with the dimensions of the image placeholder.
         var writeableBitmap = new WriteableBitmap((int)Img.Width, (int)Img.Height, 96, 96, PixelFormats.Bgra32, null);

         // Define the update square (which is as big as the entire image).
         var rect = new Int32Rect(0, 0, (int)Img.Width, (int)Img.Height);

         var pixels = new byte[(int)Img.Width * (int)Img.Height * writeableBitmap.Format.BitsPerPixel / 8];
         var rand = new Random();
         for (var y = 0; y < writeableBitmap.PixelHeight; y++)
         {
            for (var x = 0; x < writeableBitmap.PixelWidth; x++)
            {
               int alpha, red, green, blue;                              

               // Determine the pixel's color.
               if ((x % 5 == 0) || (y % 7 == 0))
               {
                  red = (int)((double)y / writeableBitmap.PixelHeight * 255);
                  green = rand.Next(100, 255);
                  blue = (int)((double)x / writeableBitmap.PixelWidth * 255);
                  alpha = 255;
               }
               else
               {
                  red = (int)((double)x / writeableBitmap.PixelWidth * 255);
                  green = rand.Next(100, 255);
                  blue = (int)((double)y / writeableBitmap.PixelHeight * 255);
                  alpha = 50;
               }

               var pixelOffset = (x + y * writeableBitmap.PixelWidth) * writeableBitmap.Format.BitsPerPixel / 8;
               pixels[pixelOffset] = (byte)blue;
               pixels[pixelOffset + 1] = (byte)green;
               pixels[pixelOffset + 2] = (byte)red;
               pixels[pixelOffset + 3] = (byte)alpha;
            }

            var stride = writeableBitmap.PixelWidth * writeableBitmap.Format.BitsPerPixel / 8;
            writeableBitmap.WritePixels(rect, pixels, stride, 0);
         }

         // Show the bitmap in an Image element.
         Img.Source = writeableBitmap;
      }

      /*
            private void cmdGenerate2_Click(object sender, RoutedEventArgs e)
            {


               // Create the bitmap, with the dimensions of the image placeholder.
               WriteableBitmap wb = new WriteableBitmap((int)Img.Width,
                   (int)Img.Height, 96, 96, PixelFormats.Bgra32, null);

               Random rand = new Random();
               for (int x = 0; x < wb.PixelWidth; x++)
               {
                  for (int y = 0; y < wb.PixelHeight; y++)
                  {
                     int alpha = 0;
                     int red = 0;
                     int green = 0;
                     int blue = 0;

                     // Determine the pixel's color.
                     if ((x % 5 == 0) || (y % 7 == 0))
                     {
                        red = (int)((double)y / wb.PixelHeight * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)x / wb.PixelWidth * 255);
                        alpha = 255;
                     }
                     else
                     {
                        red = (int)((double)x / wb.PixelWidth * 255);
                        green = rand.Next(100, 255);
                        blue = (int)((double)y / wb.PixelHeight * 255);
                        alpha = 50;
                     }

                     // Set the pixel value.                    
                     byte[] colorData = { (byte)blue, (byte)green, (byte)red, (byte)alpha }; // B G R

                     Int32Rect rect = new Int32Rect(x, y, 1, 1);
                     int stride = (wb.PixelWidth * wb.Format.BitsPerPixel) / 8;
                     wb.WritePixels(rect, colorData, stride, 0);

                     //wb.WritePixels(.[y * wb.PixelWidth + x] = pixelColorValue;
                  }
               }

               // Show the bitmap in an Image element.
               Img.Source = wb;
            }
      */
   }
}