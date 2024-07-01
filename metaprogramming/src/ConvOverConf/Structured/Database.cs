using Fundamentals;
using Humanizer;
using JetBrains.Annotations;
using MongoDB.Driver;

namespace ConvOverConf.Structured;

[Singleton]
[UsedImplicitly]
public class Database : IDatabase
{
   private readonly IMongoDatabase _mongoDatabase;

   public Database()
   {
      var client = new MongoClient("mongodb://localhost:27017");
      _mongoDatabase = client.GetDatabase("TheSystem");
   }

   public IMongoCollection<T> GetCollectionFor<T>() => _mongoDatabase.GetCollection<T>(typeof(T).Name.Pluralize());
}