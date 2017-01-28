using System.Windows;
using System.Windows.Media;

namespace CustomControlsClient
{
   public partial class ColorPickerUserControlTest
   {
      public ColorPickerUserControlTest()
      {
         InitializeComponent();
      }

      void OnGetClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show(colorPicker.Color.ToString());
      }

      void OnSetClick(object sender, RoutedEventArgs e)
      {
         colorPicker.Color = Colors.Beige;
      }

      void OnColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
      {
         if (lblColor != null)
            lblColor.Text = string.Format("The new color is {0}", e.NewValue);
      }
   }
}