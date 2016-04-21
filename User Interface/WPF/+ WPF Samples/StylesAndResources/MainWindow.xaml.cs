using System.Windows;

namespace StylesAndResources
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnResources(object sender, RoutedEventArgs e)
      {
         new ResourceDemo().Show();
      }
   }
}
