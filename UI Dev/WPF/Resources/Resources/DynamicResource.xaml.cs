using System.Windows;
using System.Windows.Media;

namespace Resources
{
   public partial class DynamicResource
   {
      public DynamicResource()
      {
         InitializeComponent();
      }

      private void OnResourceChange(object sender, RoutedEventArgs e)
      {
         Resources["TileBrush"] = new SolidColorBrush(Colors.LightBlue);

         //ImageBrush brush = (ImageBrush)this.Resources["TileBrush"];
         //brush.Viewport = new Rect(0, 0, 5, 5);            
      }
   }
}