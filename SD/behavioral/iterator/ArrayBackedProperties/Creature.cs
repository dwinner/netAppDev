using System.Collections;

namespace ArrayBackedProperties;

public class Creature : IEnumerable<int>
{
   private const int StrengthIdx = 0;
   private readonly int[] _statsArray = new int[3];

   public IEnumerable<int> Stats => _statsArray;

   public int Strength
   {
      get => _statsArray[StrengthIdx];
      set => _statsArray[StrengthIdx] = value;
   }

   public int Agility { get; set; }
   public int Intelligence { get; set; }
   public double AverageStat => _statsArray.Average();

   //public double AverageStat => SumOfStats / 3.0;

   //public double SumOfStats => Strength + Agility + Intelligence;
   public double SumOfStats => _statsArray.Sum();

   //public double MaxStat => Math.Max(
   //  Math.Max(Strength, Agility), Intelligence);

   public double MaxStat => _statsArray.Max();

   public int this[int index]
   {
      get => _statsArray[index];
      set => _statsArray[index] = value;
   }

   public IEnumerator<int> GetEnumerator()
   {
      var enumerator = _statsArray.AsEnumerable().GetEnumerator();
      using (enumerator)
      {
         return enumerator;
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}