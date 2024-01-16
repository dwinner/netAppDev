namespace OrderComparisonSample;

public readonly struct Note(int semitonesFromA) 
   : IComparable<Note>, IEquatable<Note>, IComparable
{
   public int SemitonesFromA { get; } = semitonesFromA;

   public int CompareTo(Note other) // Generic IComparable<T>
   {
      if (Equals(other))
      {
         return 0; // Fail-safe check
      }

      return SemitonesFromA.CompareTo(other.SemitonesFromA);
   }

   int IComparable.CompareTo(object? other) // Nongeneric IComparable
   {
      if (other is not Note note)
      {
         throw new InvalidOperationException("CompareTo: Not a note");
      }

      return CompareTo(note);
   }

   public static bool operator <(Note n1, Note n2)
      => n1.CompareTo(n2) < 0;

   public static bool operator >(Note n1, Note n2)
      => n1.CompareTo(n2) > 0;

   public bool Equals(Note other) // for IEquatable<Note>
      => SemitonesFromA == other.SemitonesFromA;

   public override bool Equals(object? other)
   {
      if (other is not Note note)
      {
         return false;
      }

      return Equals(note);
   }

   public override int GetHashCode()
      => SemitonesFromA.GetHashCode();

   public static bool operator ==(Note n1, Note n2)
      => Equals(n1, n2);

   public static bool operator !=(Note n1, Note n2)
      => !(n1 == n2);
}