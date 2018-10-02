using System.Windows;

namespace StylesAndResources
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnResources(object sender, RoutedEventArgs e)
      {
         new ResourceDemoWindow().Show();
      }
   }
}