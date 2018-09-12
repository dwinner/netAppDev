using System.Windows;

namespace TriggerDemo
{
   public partial class MainWindow
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