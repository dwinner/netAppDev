using System.Drawing;

namespace HitTesting
{
   internal class MyCircle : MyShape
   {
      private readonly int _radius;
      private Point _center;

      public MyCircle(Point center, int radius)
      {
         _center = center;
         _radius = radius;
      }

      public override void Draw(Graphics graphics)
      {
         graphics.DrawEllipse(Pens.Black, _center.X - _radius, _center.Y - _radius,
            _radius*2, _radius*2);
      }

      public override bool HitTest(Point location)
      {
         /* X^2 + Y^2 = R^2 is the formula for a circle.
             * where R is the radius
             * A point is in the circle if X^2 + Y^2 <= R^2
             * 
             * This all assumes the location is 0,0 so be sure to normalize to that
             */
         var normalized = new Point(location.X - _center.X, location.Y - _center.Y);

         return normalized.X*normalized.X + normalized.Y*normalized.Y <= (_radius*_radius);
      }

      public override string ToString()
      {
         return "MyCircle";
      }
   }
}