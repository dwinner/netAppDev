using System.Windows;

namespace LocalizableApplication
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnCmdClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(Resources["Error"].ToString());
      }
   }
}