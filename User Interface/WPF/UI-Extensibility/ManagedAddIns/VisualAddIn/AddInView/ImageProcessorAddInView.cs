using System.AddIn.Pipeline;
using System.IO;
using System.Windows;

namespace AddInView
{
   [AddInBase]
   public abstract class ImageProcessorAddInView
   {
      public abstract FrameworkElement GetVisual(Stream imageStream);
   }
}
