using System.Windows;

namespace Controls
{
   public partial class ScrollableTextBoxColumn
   {
      public ScrollableTextBoxColumn()
      {
         InitializeComponent();
      }

      private void LineUp(object sender, RoutedEventArgs e)
      {
         Scroller.LineUp();
      }

      private void LineDown(object sender, RoutedEventArgs e)
      {
         Scroller.LineDown();
      }

      private void PageUp(object sender, RoutedEventArgs e)
      {
         Scroller.PageUp();
      }

      private void PageDown(object sender, RoutedEventArgs e)
      {
         Scroller.PageDown();
      }
   }
}