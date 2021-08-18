using System.Windows;

namespace WindowPageHost
{
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void OnClose(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}