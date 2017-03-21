using System.AddIn;
using AddInView;
using JetBrains.Annotations;

namespace NegativeImageAddIn
{
   [AddIn("Negative Image Processor",
      Version = "1.0.0.0",
      Publisher = "Imaginomics",
      Description = "Inverts colors to look like a photo negative")]
   [UsedImplicitly]
   public class NegativeImageProcessor : ImageProcessorAddInView
   {
      private HostObject _host;

      public override byte[] ProcessImageBytes(byte[] pixels)
      {
         var iteration = pixels.Length / 100;

         for (var i = 0; i < pixels.Length - 2; i++)
         {
            pixels[i] = (byte) (255 - pixels[i]);
            pixels[i + 1] = (byte) (255 - pixels[i + 1]);
            pixels[i + 2] = (byte) (255 - pixels[i + 2]);

            if (i % iteration == 0)
               _host.ReportProgress(i / iteration);
         }

         return pixels;
      }

      public override void Initialize(HostObject hostObj)
      {
         _host = hostObj;
      }
   }
}