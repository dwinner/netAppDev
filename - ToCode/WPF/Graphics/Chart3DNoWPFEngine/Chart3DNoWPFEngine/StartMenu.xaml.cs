using System.Windows;

namespace Chart3DNoWPFEngine
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

      private void Cube_Click(object sender, RoutedEventArgs e)
      {
         Cube cube = new Cube();
         cube.ShowDialog();
      }

      private void Coordinates_Click(object sender, RoutedEventArgs e)
      {
         Coordinates3D c3d = new Coordinates3D();
         c3d.ShowDialog();
      }

      private void Line3D_Click(object sender, RoutedEventArgs e)
      {
         Line3D line3d = new Line3D();
         line3d.ShowDialog();
      }

      private void Surface_Click(object sender, RoutedEventArgs e)
      {
         SurfaceChart sc = new SurfaceChart();
         sc.ShowDialog();
      }

      private void Close_Click(object sender, RoutedEventArgs e)
      {
         this.Close();
      }
   }
}
