using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace DrawingIn3D
{   
   public partial class CubeMesh
   {
      public CubeMesh()
      {
         InitializeComponent();
      }

/*
      private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
      {
         var v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
         var v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
         return Vector3D.CrossProduct(v0, v1);
      }
*/

      private void OnSetMesh(object sender, RoutedEventArgs e)
      {
         CubeGeometry.Geometry = (Geometry3D) ((Button) sender).Tag;
      }
   }
}