using System.Windows;

namespace WpfCommandLink
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void CommandLink_OnClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Ok");
      }
   }
}