namespace CheckedOpVersion;

public struct Note
{
   public int SemitonesFromA { get; }

   public Note(int semitonesFromA) => SemitonesFromA = semitonesFromA;

   public static Note operator +(Note x, int semitones)
      => new(x.SemitonesFromA + semitones);

   public static Note operator checked +(Note x, int semitones)
      => checked(new(x.SemitonesFromA + semitones));
}