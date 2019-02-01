using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace DrawingIn3D
{
   public partial class HitTesting
   {
      public HitTesting()
      {
         InitializeComponent();
      }

      private void OnRingVisualMouseDown(object sender, MouseButtonEventArgs e)
      {
         var location = e.GetPosition(Viewport);
         var meshHitResult = (RayMeshGeometry3DHitTestResult) VisualTreeHelper.HitTest(Viewport, location);
         AxisRotation.Axis = new Vector3D(
            -meshHitResult.PointHit.Y, meshHitResult.PointHit.X, 0);

         var animation = new DoubleAnimation
         {
            To = 40,
            DecelerationRatio = 1,
            Duration = TimeSpan.FromSeconds(0.15),
            AutoReverse = true
         };

         AxisRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
      }

/*
      private void OnViewportMouseDown(object sender, MouseButtonEventArgs e)
      {
         var location = e.GetPosition(viewport);
         var hitResult = VisualTreeHelper.HitTest(viewport, location);

         if (hitResult != null && Equals(hitResult.VisualHit, ringVisual))
         {
            // Hit the ring.
         }

         var meshHitResult = hitResult as RayMeshGeometry3DHitTestResult;
         if (meshHitResult != null && Equals(meshHitResult.ModelHit, ringModel))
         {
            // Hit the ring.
         }

         if (meshHitResult != null && Equals(meshHitResult.MeshHit, ringMesh))
         {
            axisRotation.Axis = new Vector3D(
               -meshHitResult.PointHit.Y, meshHitResult.PointHit.X, 0);

            var animation = new DoubleAnimation
            {
               To = 40,
               DecelerationRatio = 1,
               Duration = TimeSpan.FromSeconds(0.15),
               AutoReverse = true
            };

            axisRotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
         }
      }
*/
   }
}