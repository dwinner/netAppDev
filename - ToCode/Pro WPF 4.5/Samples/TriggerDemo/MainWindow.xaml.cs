using System.Windows;

namespace TriggerDemo
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

      private void OnPropertyTrigger(object sender, RoutedEventArgs e)
      {
         new PropertyTriggerWindow().Show();
      }

      private void OnMultiTrigger(object sender, RoutedEventArgs e)
      {
         new MultiTriggerWindow().Show();
      }

      private void OnDataTrigger(object sender, RoutedEventArgs e)
      {
         new DataTriggerWindow().Show();
      }
   }
}
