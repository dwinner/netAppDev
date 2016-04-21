using System.Windows;

namespace BrushesDemo
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

      private void OnReflectedButton(object sender, RoutedEventArgs e)
      {
         reflected.Play();
      }
   }
}
