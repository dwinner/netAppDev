using System.IO;
using System.Windows;

namespace HostView
{
   public abstract class ImageProcessorHostView
   {
      public abstract FrameworkElement GetVisual(Stream imageStream);
   }
}
