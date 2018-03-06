using System.Drawing;

namespace HitTesting
{
   internal class MyEllipse : MyShape
   {
      private readonly int _xRadius;
      private readonly int _yRadius;
      private Point _center;

      public MyEllipse(Point center, int xRadius, int yRadius)
      {
         _center = center;
         _xRadius = xRadius;
         _yRadius = yRadius;
      }

      public override void Draw(Graphics graphics)
      {
         graphics.DrawEllipse(Pens.Black,
            new Rectangle(_center.X - _xRadius, _center.Y - _yRadius,
               _xRadius*2, _yRadius*2));
      }

      public override bool HitTest(Point location)
      {
         if (_xRadius <= 0.0 || _yRadius <= 0.0)
            return false;
         /* This is a more general form of the circle equation
             * 
             * X^2/a^2 + Y^2/b^2 <= 1
             */

         var normalized = new Point(location.X - _center.X, location.Y - _center.Y);

         return ((double) (normalized.X*normalized.X)/(_xRadius*_xRadius)) +
                ((double) (normalized.Y*normalized.Y)/(_yRadius*_yRadius))
                <= 1.0f;
      }

      public override string ToString()
      {
         return "MyEllipse";
      }
   }
}