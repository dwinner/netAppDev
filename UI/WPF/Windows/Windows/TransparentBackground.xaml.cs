using System.Windows;
using System.Windows.Input;

namespace Windows
{
   public partial class TransparentBackground
   {
      public TransparentBackground()
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