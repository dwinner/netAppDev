/**
 * Локализация приложений WPF с использованием ресурсов
 */

using System.Windows;

namespace WPF.UsingResources
{   
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void LocButton_OnClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(Properties.Resources.LocButtonMessage);
      }
   }
}
