using System;
using System.Windows.Media.Imaging;

namespace _03_ImageProcessorPipeline.Infrastructure
{
   public class ProcessedImageEventArgs : EventArgs
   {
      public ProcessedImageEventArgs(BitmapSource image) => Image = image;

      public BitmapSource Image { get; }
   }
}