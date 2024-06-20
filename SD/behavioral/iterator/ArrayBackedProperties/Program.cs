namespace ArrayBackedProperties;

internal class Program
{
   private static void Main()
   {
      var creature = new Creature
      {
         Strength = 10,
         Intelligence = 11,
         Agility = 12
      };
      Console.WriteLine($"Creature has average stat = {creature.AverageStat}, " +
                        $"Max stat = {creature.MaxStat}, " +
                        $"Sum of stats = {creature.SumOfStats}.");
   }
}