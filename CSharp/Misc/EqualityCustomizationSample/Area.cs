namespace EqualityCustomizationSample;

public struct Area(int measure1, int measure2) : IEquatable<Area>
{
   public readonly int Measure1 = Math.Min(measure1, measure2);
   public readonly int Measure2 = Math.Max(measure1, measure2);

   public override bool Equals(object? other)
      => other is Area a && Equals(a);

   public bool Equals(Area other) // Implements IEquatable<Area>
      => Measure1 == other.Measure1 && Measure2 == other.Measure2;

   public override int GetHashCode()
      => HashCode.Combine(Measure1, Measure2);

   public static bool operator ==(Area a1, Area a2) => Equals(a1, a2);

   public static bool operator !=(Area a1, Area a2) => !(a1 == a2);
}