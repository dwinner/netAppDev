/**
 * Генерирование миниатюры
 */

using System;
using System.Drawing;
using System.IO;

namespace GenerateThumbnail
{
   internal static class Program
   {
      private static void PrintUsage()
      {
         Console.WriteLine("Usage: GenerateThumbnail.exe inputfile maxSize");
      }

      private static void Main(string[] args)
      {
         if (args.Length < 2)
         {
            PrintUsage();
            return;
         }

         if (!File.Exists(args[0]))
         {
            PrintUsage();
            return;
         }

         var outputFile = Path.GetFileNameWithoutExtension(args[0]) + "_thumb" + Path.GetExtension(args[0]);

         int maxSize;
         if (!int.TryParse(args[1], out maxSize))
         {
            PrintUsage();
            return;
         }

         try
         {
            var image = Image.FromFile(args[0]);

            var size = CalculateThumbSize(image.Size, maxSize);

            var thumbnail = image.GetThumbnailImage(size.Width, size.Height, ThumbnailAbortCallback, IntPtr.Zero);
            thumbnail.Save(outputFile);
         }
         catch (OutOfMemoryException ex)
         {
            Console.WriteLine(ex);
         }
         catch (FileNotFoundException ex)
         {
            Console.WriteLine(ex);
         }
      }

      //called by image processor to know if it should stop the resizing
      //could be useful with large images
      private static bool ThumbnailAbortCallback()
      {
         return false;
      }

      //get the proportional size of the resulting image, based on the maxSize the user passed in
      private static Size CalculateThumbSize(Size size, int maxSize)
      {
         return size.Width > size.Height
            ? new Size(maxSize, (int)((size.Height / (double)size.Width) * maxSize))
            : new Size((int)((size.Width / (double)size.Height) * maxSize), maxSize);
      }
   }
}