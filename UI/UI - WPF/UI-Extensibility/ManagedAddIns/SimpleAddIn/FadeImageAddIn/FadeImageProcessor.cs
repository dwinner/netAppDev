using System;
using System.AddIn;
using AddInView;

namespace FadeImageAddIn
{
   [AddIn("Fade Image Processor",
      Version = "1.0.0.0",
      Publisher = "SupraImage",
      Description = "Darkens the picture")]
   public class FadeImageProcessor : ImageProcessorAddInView
   {
      public override byte[] ProcessImageBytes(byte[] pixels)
      {
         var rand = new Random();
         var offset = rand.Next(0, 10);
         for (var i = 0; i < pixels.Length - 1 - offset; i++)
            if ((i + offset) % 5 == 0)
               pixels[i] = 0;

         return pixels;
      }
   }
}