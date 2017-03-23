using System.Windows;

namespace BrushesDemo
{
   public partial class MainWindow
   {
      public MainWindow()
      {
         InitializeComponent();
      }

      private void OnReflectedButton(object sender, RoutedEventArgs e)
      {
         ReflectedMedia.Play();
      }
   }
}