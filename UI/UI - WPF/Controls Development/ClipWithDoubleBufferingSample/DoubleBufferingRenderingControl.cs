using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClipWithDoubleBufferingSample
{
   public class DoubleBufferingRenderingControl : Control
   {
      static DoubleBufferingRenderingControl() => DefaultStyleKeyProperty.OverrideMetadata(
         typeof(DoubleBufferingRenderingControl),
         new FrameworkPropertyMetadata(typeof(DoubleBufferingRenderingControl)));

      protected override void OnRender(DrawingContext drawingContext)
      {
         base.OnRender(drawingContext);

         var clippingArea = new Rect(new Point(.0, .0), new Size(ActualWidth, ActualHeight));
         var clippingGroup = new DrawingGroup {ClipGeometry = new RectangleGeometry(clippingArea)};
         var doubleBuffering = new DoubleBufferingDrawing();
         clippingGroup.Children.Add(doubleBuffering.Drawing);
         drawingContext.DrawDrawing(clippingGroup);
      }
   }
}