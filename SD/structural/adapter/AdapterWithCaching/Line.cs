namespace AdapterWithCaching;

public class Line(Point start, Point end) : IEquatable<Line>
{
   public Point Start { get; } = start;

   public Point End { get; } = end;

   public bool Equals(Line? other) => other != null && Equals(Start, other.Start) && Equals(End, other.End);

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

      return Equals((Line)obj);
   }

   public override int GetHashCode()
   {
      unchecked
      {
         const int hashSeed = 397;
         return (Start.GetHashCode() * hashSeed) ^ End.GetHashCode();
      }
   }
}