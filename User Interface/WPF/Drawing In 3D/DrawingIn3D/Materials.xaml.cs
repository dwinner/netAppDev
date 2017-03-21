using System.Windows;
using System.Windows.Media.Media3D;

namespace DrawingIn3D
{   
   public partial class Materials
   {
      public Materials()
      {
         InitializeComponent();
      }

      private void OnChangeMaterial(object sender, RoutedEventArgs e)
      {
         MaterialsGroup.Children.Clear();
         Rect.Visibility = BackgroundCheckBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden;

         if (DiffuseCheckBox.IsChecked == true)
            MaterialsGroup.Children.Add((Material) FindResource("Diffuse"));

         if (SpecularCheckBox.IsChecked == true)
            MaterialsGroup.Children.Add((Material) FindResource("Specular"));

         if (EmissiveCheckBox.IsChecked == true)
            MaterialsGroup.Children.Add((Material) FindResource("Emissive"));
      }
   }
}