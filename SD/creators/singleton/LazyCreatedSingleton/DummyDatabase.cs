namespace LazyCreatedSingleton;

public class DummyDatabase : IDatabase
{
   public int GetPopulation(string name) =>
      new Dictionary<string, int>
      {
         ["alpha"] = 1,
         ["beta"] = 2,
         ["gamma"] = 3
      }[name];
}