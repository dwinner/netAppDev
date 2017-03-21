using System.AddIn;
using System.IO;
using System.Windows;
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
      public override FrameworkElement GetVisual(Stream imageStream)
      {
         return new ImagePreview(imageStream);
      }
   }
}