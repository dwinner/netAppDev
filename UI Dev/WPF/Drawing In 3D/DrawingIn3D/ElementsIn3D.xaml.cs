using System.Windows;

namespace DrawingIn3D
{
   public partial class ElementsIn3D
   {
      public ElementsIn3D()
      {
         InitializeComponent();
      }

      private void OnClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Button clicked.");
      }
   }
}