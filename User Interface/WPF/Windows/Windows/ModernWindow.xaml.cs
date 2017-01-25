using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Windows
{
   public partial class ModernWindow
   {
      private bool _isWiden;

      public ModernWindow()
      {
         InitializeComponent();
      }

      private void OnInitiateWiden(object sender, MouseEventArgs e)
      {
         _isWiden = true;
      }

      private void OnEndWiden(object sender, MouseEventArgs e)
      {
         _isWiden = false;

         // Make sure capture is released.
         var rect = (Rectangle) sender;
         rect.ReleaseMouseCapture();
      }

      private void OnWiden(object sender, MouseEventArgs e)
      {
         var rect = (Rectangle) sender;
         if (_isWiden)
         {
            rect.CaptureMouse();
            var newWidth = e.GetPosition(this).X + 5;
            if (newWidth > 0)
               Width = newWidth;
         }
      }

      private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}