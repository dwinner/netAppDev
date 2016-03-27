using System;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
   public class ProcessedImageEventArgs : EventArgs
   {
      public BitmapSource Img { get; private set; }

      public ProcessedImageEventArgs(BitmapSource img)
      {
         Img = img;
      }
   }
}