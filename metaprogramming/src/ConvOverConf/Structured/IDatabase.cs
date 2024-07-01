using MongoDB.Driver;

namespace ConvOverConf.Structured;

public interface IDatabase
{
   IMongoCollection<T> GetCollectionFor<T>();
}