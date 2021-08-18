using System.Windows;

namespace HostingActiveXviaXaml
{
   public partial class Window1
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void OnConnect(object sender, RoutedEventArgs e)
      {
         TermServ.Server = ServerBox.Text;
         TermServ.Connect();
      }
   }
}