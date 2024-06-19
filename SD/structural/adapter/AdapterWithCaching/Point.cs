namespace AdapterWithCaching;

public class Point(int x, int y) : IEquatable<Point>
{
   public int X { get; } = x;
   public int Y { get; } = y;

   public bool Equals(Point? other) => other != null && X == other.X && Y == other.Y;

   public override bool Equals(object? obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      if (ReferenceEquals(this, obj))
      {
         return true;
      }

      if (obj.GetType() != GetType())
      {
         return false;
      }

      return Equals((Point)obj);
   }

   public override int GetHashCode()
   {
      unchecked
      {
         return (X * 397) ^ Y;
      }
   }

   public override string ToString() => $"({X}, {Y})";
}