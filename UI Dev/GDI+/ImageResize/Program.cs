/**
 * Изменение размеров изображения
 */

using System;
using System.Drawing;
using System.IO;

namespace ImageResize
{
   internal static class Program
   {
      private static void PrintUsage()
      {
         Console.WriteLine("Usage: ImageResize.exe inputfile width height");
      }

      private static void Main(string[] args)
      {
         if (args.Length < 3)
         {
            PrintUsage();
            return;
         }

         if (!File.Exists(args[0]))
         {
            PrintUsage();
            return;
         }

         var newWidth = int.Parse(args[1]);
         var newHeight = int.Parse(args[2]);

         var outputFile = string.Format("{0}_{1}x{2}{3}",
            Path.GetFileNameWithoutExtension(args[0]), newWidth, newHeight, Path.GetExtension(args[0]));

         Bitmap resizedBmp = null;
         try
         {
            var image = Image.FromFile(args[0]);
            resizedBmp = new Bitmap(image, new Size(newWidth, newHeight));
            resizedBmp.Save(outputFile, image.RawFormat);
         }
         finally
         {
            if (resizedBmp != null)
            {
               resizedBmp.Dispose();
            }
         }
      }
   }
}