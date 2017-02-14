using System.Windows;

namespace HostingActiveXviaXaml
{
   public partial class Window1 : Window
   {
      public Window1()
      {
         InitializeComponent();
      }

      void connectButton_Click(object sender, RoutedEventArgs e)
      {
         termServ.Server = serverBox.Text;
         termServ.Connect();
      }
   }
}