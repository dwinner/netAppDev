using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CustomBehaviorsLibrary
{
   public class DragInCanvasBehavior : Behavior<UIElement>
   {
      private Canvas _canvas;

      // Keep track of when the element is being dragged.
      private bool _isDragging;

      // When the element is clicked, record the exact position
      // where the click is made.
      private Point _mouseOffset;

      protected override void OnAttached()
      {
         base.OnAttached();

         // Hook up event handlers.            
         AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
         AssociatedObject.MouseMove += AssociatedObject_MouseMove;
         AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
      }

      protected override void OnDetaching()
      {
         base.OnDetaching();

         // Detach event handlers.
         AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
         AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
         AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
      }

      private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         // Find the canvas.
         if (_canvas == null) _canvas = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;

         // Dragging mode begins.
         _isDragging = true;

         // Get the position of the click relative to the element
         // (so the top-left corner of the element is (0,0).
         _mouseOffset = e.GetPosition(AssociatedObject);

         // Capture the mouse. This way you'll keep receiveing
         // the MouseMove event even if the user jerks the mouse
         // off the element.
         AssociatedObject.CaptureMouse();
      }

      private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
      {
         if (_isDragging)
         {
            // Get the position of the element relative to the Canvas.
            var point = e.GetPosition(_canvas);

            // Move the element.
            AssociatedObject.SetValue(Canvas.TopProperty, point.Y - _mouseOffset.Y);
            AssociatedObject.SetValue(Canvas.LeftProperty, point.X - _mouseOffset.X);
         }
      }

      private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
      {
         if (_isDragging)
         {
            AssociatedObject.ReleaseMouseCapture();
            _isDragging = false;
         }
      }
   }
}