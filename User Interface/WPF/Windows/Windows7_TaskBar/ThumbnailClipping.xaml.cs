using System.Windows;
using System.Windows.Controls;

namespace Windows7_TaskBar
{
   public partial class ThumbnailClipping
   {
      public ThumbnailClipping()
      {
         InitializeComponent();
      }

      private void OnShrinkPreviewClick(object sender, RoutedEventArgs e)
      {
         // Find the position of the clicked button, in window coordinates.
         var cmd = (Button) sender;
         var locationFromWindow = cmd.TranslatePoint(new Point(0, 0), this);

         // Determine the width that should be added to every side.
         var left = locationFromWindow.X;
         var top = locationFromWindow.Y;
         var right = LayoutRoot.ActualWidth - cmd.ActualWidth - left;
         var bottom = LayoutRoot.ActualHeight - cmd.ActualHeight - top;

         // Apply the clipping.
         TaskBarItem.ThumbnailClipMargin = new Thickness(left, top, right, bottom);
      }
   }
}