namespace RefReturn
{
   internal class Vector
   {
      private Point3d _location;
      private int _magnitude;
      public Point3d Location { get; set; }
      public ref Point3d RefLocation => ref _location;

      public int Magnitude
      {
         get => _magnitude;
         set => _magnitude = value;
      }

      public ref int RefMagnitude => ref _magnitude;
   }
}