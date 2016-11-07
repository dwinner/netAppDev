using System.Windows;

namespace ColorsAndBrushes
{
   /// <summary>
   /// Interaction logic for StartMenu.xaml
   /// </summary>
   public partial class StartMenu : Window
   {
      public StartMenu()
      {
         InitializeComponent();
      }

      private void CustomShading_Click(object sender, RoutedEventArgs e)
      {
         CustomColorShading shading = new CustomColorShading();
         shading.ShowDialog();
      }

      private void CustomBrush_Click(object sender, RoutedEventArgs e)
      {
         CustomColormapBrushes ccb = new CustomColormapBrushes();
         ccb.ShowDialog();
      }

      private void Close_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }

      private void Color_Click(object sender, RoutedEventArgs e)
      {
         SystemColors sc = new SystemColors();
         sc.ShowDialog();
      }

      private void ColorPicker_Click(object sender, RoutedEventArgs e)
      {
         ColorPicker cp = new ColorPicker();
         cp.ShowDialog();
      }

      private void SolidColorBrush_Click(object sender, RoutedEventArgs e)
      {
         SolidColorBrushes scb = new SolidColorBrushes();
         scb.ShowDialog();
      }

      private void LinearGradient_Click(object sender, RoutedEventArgs e)
      {
         LinearGradientBrushes lgb = new LinearGradientBrushes();
         lgb.ShowDialog();
      }

      private void RadialGradient_Click(object sender, RoutedEventArgs e)
      {
         RadialGradientBrushes rgb = new RadialGradientBrushes();
         rgb.ShowDialog();
      }

      private void Drawing_Click(object sender, RoutedEventArgs e)
      {
         DrawingBrushes db = new DrawingBrushes();
         db.ShowDialog();
      }
   }
}
