using System.Windows;
using System.Windows.Media;

namespace ClipWithDoubleBufferingSample
{
   public sealed class DoubleBufferingDrawing : DrawingVisual
   {
      public DoubleBufferingDrawing()
      {
         using (var drawingContext = RenderOpen())
         {
            drawingContext.DrawRectangle(Brushes.Aqua, new Pen(Brushes.Black, 2.0), new Rect(0, 0, 100, 100));
         }
      }
   }
}