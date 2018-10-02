using System.Windows;

namespace LayoutDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnShowStackPanel(object sender, RoutedEventArgs e)
      {
         new StackPanelWindow().Show();
      }

      private void OnShowWrapPanel(object sender, RoutedEventArgs e)
      {
         new WrapPanelWindow().Show();
      }

      private void OnShowCanvas(object sender, RoutedEventArgs e)
      {
         new CanvasWindow().Show();
      }

      private void OnShowDockPanel(object sender, RoutedEventArgs e)
      {
         new DockPanelWindow().Show();
      }

      private void OnShowGrid(object sender, RoutedEventArgs e)
      {
         new GridWindow().Show();
      }
   }
}