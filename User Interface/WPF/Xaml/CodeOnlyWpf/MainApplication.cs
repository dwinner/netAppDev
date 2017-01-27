using System.Windows;

namespace CodeOnlyWpf
{
   public class MainApplication : Application
   {
      public MainApplication()
      {
         var window = new MainWindow();
         window.ShowDialog();
      }
   }
}