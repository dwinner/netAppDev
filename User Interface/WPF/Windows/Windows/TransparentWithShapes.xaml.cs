using System.Windows;
using System.Windows.Input;

namespace Windows
{
   public partial class TransparentWithShapes
   {
      public TransparentWithShapes()
      {
         InitializeComponent();
      }

      private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}