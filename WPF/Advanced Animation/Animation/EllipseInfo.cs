using System.Windows.Media;
using System.Windows.Shapes;

namespace Animation
{
   public class EllipseInfo
   {
      public const int EllipseRadius = 10;

      public EllipseInfo(Ellipse ellipse, double velocityY)
      {
         VelocityY = velocityY;
         Ellipse = ellipse;
      }

      public Ellipse Ellipse { get; private set; }

      public double VelocityY { get; set; }

      public static Ellipse CreateEllipse()
      {
         return new Ellipse
         {
            Fill = Brushes.LimeGreen,
            Width = EllipseRadius,
            Height = EllipseRadius
         };
      }
   }
}