using System.Windows;
using System.Windows.Input;

namespace TransparentFormWPF
{
   public partial class TransparentWindow
   {
      public TransparentWindow()
      {
         InitializeComponent();
      }

      protected override void OnMouseDown(MouseButtonEventArgs e)
      {
         base.OnMouseDown(e);
         DragMove();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}