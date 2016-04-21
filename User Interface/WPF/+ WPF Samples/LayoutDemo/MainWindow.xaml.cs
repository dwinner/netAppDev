using System.Windows;

namespace LayoutDemo
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

      private void ShowStackPanel(object sender, RoutedEventArgs e)
      {
         new StackPanelWindow().Show();
      }

      private void ShowWrapPanel(object sender, RoutedEventArgs e)
      {
         new WrapPanelWindow().Show();
      }

      private void ShowCanvas(object sender, RoutedEventArgs e)
      {
         new CanvasWindow().Show();
      }

      private void ShowDockPanel(object sender, RoutedEventArgs e)
      {
         new DockPanelWindow().Show();
      }

      private void ShowGrid(object sender, RoutedEventArgs e)
      {
         new GridWindow().Show();
      }

   }
}
