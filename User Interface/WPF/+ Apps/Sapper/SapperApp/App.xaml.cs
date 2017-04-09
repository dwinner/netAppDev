using System.Windows;
using Sapper.Windows;

namespace SapperApp
{
   public partial class App
   {
      private void OnStartup(object sender, StartupEventArgs e)
      {
         MainWindow = new SapperWindow();
         MainWindow.Show();
      }
   }
}