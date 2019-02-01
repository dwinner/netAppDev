using System.AddIn;
using AddInView;

namespace NegativeImageAddIn
{
   [AddIn("Negative Image Processor",
      Version = "1.0.0.0",
      Publisher = "Imaginomics",
      Description = "Inverts colors to look like a photo negative")]
   public class NegativeImageProcessor : ImageProcessorAddInView
   {
      public override byte[] ProcessImageBytes(byte[] pixels)
      {
         const int byteThreshold = 0xFF;

         for (var i = 0; i < pixels.Length - 2; i++)
         {
            pixels[i] = (byte) (byteThreshold - pixels[i]);
            pixels[i + 1] = (byte) (byteThreshold - pixels[i + 1]);
            pixels[i + 2] = (byte) (byteThreshold - pixels[i + 2]);
         }

         return pixels;
      }
   }
}