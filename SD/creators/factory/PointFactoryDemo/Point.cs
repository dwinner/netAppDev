namespace PointFactoryDemo;

public class Point
{
   // factory method
   public enum CoordinateSystem
   {
      Cartesian,
      Polar
   }

   // constant field
   public static Point Origin2 = new(0, 0);
   private double x, y;

   protected Point(double x, double y)
   {
      this.x = x;
      this.y = y;
   }

   // names do not communicate intent
   public Point(double a, double b, CoordinateSystem cs = CoordinateSystem.Cartesian)
   {
      switch (cs)
      {
         case CoordinateSystem.Polar:
            x = a * Math.Cos(b);
            y = a * Math.Sin(b);
            break;
         
         case CoordinateSystem.Cartesian:
            x = a;
            y = b;
            break;

         default:
            goto case CoordinateSystem.Cartesian;
      }

      // steps to add a new system
      // 1. augment CoordinateSystem
      // 2. change ctor
   }

   // factory property

   public static Point Origin => new(0, 0);

   public override string ToString() => $"{nameof(x)}: {x}, {nameof(y)}: {y}";

   // make it lazy
   public static class Factory
   {
      public static Point NewCartesianPoint(double x, double y) => new(x, y);

      public static Point NewPolarPoint(double rho, double theta) => new(rho * Math.Cos(theta), rho * Math.Sin(theta));
   }
}