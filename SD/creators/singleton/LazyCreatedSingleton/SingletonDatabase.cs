using MoreLinq;

namespace LazyCreatedSingleton;

public class SingletonDatabase : IDatabase
{
   // laziness + thread safety
   private static readonly Lazy<SingletonDatabase> Impl = new(() =>
   {
      Count++;
      return new SingletonDatabase();
   });

   private readonly Dictionary<string, int> _capitals;

   private SingletonDatabase()
   {
      Console.WriteLine("Initializing database");

      _capitals = File.ReadAllLines(
            Path.Combine(
               new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt")
         )
         .Batch(2)
         .ToDictionary(
            list => list.ElementAt(0).Trim(),
            list => int.Parse(list.ElementAt(1)));
   }

   public static int Count { get; private set; }

   public static IDatabase Instance => Impl.Value;

   public int GetPopulation(string name) => _capitals[name];
}