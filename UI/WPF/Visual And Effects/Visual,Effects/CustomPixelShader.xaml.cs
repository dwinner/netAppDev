using System.Windows;

namespace Drawing
{
   public partial class CustomPixelShader
   {
      public CustomPixelShader()
      {
         InitializeComponent();
      }

      private void OnEffectClick(object sender, RoutedEventArgs e)
      {
         Img.Effect = EffectCheckBox.IsChecked != true ? null : new GrayscaleEffect();
      }
   }
}