namespace ClassSample
{
   public class Vertex3D
   {
      private static int _numInstances = 0;

      public double X { get; set; }

      public double Y { get; set; }

      public double Z { get; set; }

      public void SetToOrigin()
      {
         X = Y = Z = 0.0;
      }

      public Vertex3D()
         : this(0.0, 0.0, 0.0)
      { }

      public Vertex3D(double x, double y, double z)
      {
         X = x;
         Y = y;
         Z = z;
      }

      public Vertex3D(System.Drawing.Point point)
         : this(point.X, point.Y, 0)
      {
      }

      public static Vertex3D Add(Vertex3D a, Vertex3D b)
      {
         var result = new Vertex3D { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
         return result;
      }
   }
}
