using CQRS.Core.Domain;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Cmd.Infrastructure.Config;

namespace Post.Cmd.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
   private readonly IMongoCollection<EventModel> _eventStoreCollection;

   public EventStoreRepository(IOptions<MongoDbConfig> config)
   {
      var mongoClient = new MongoClient(config.Value.ConnectionString);
      var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);
      _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
   }

   public async Task<List<EventModel>> FindAllAsync()
   {
      return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
   }

   public async Task<List<EventModel>> FindByAggregateId(Guid anAggregateId)
   {
      return await _eventStoreCollection.Find(x => x.AggregateIdentifier == anAggregateId).ToListAsync()
         .ConfigureAwait(false);
   }

   public async Task SaveAsync(EventModel anEventModel)
   {
      await _eventStoreCollection.InsertOneAsync(anEventModel).ConfigureAwait(false);
   }
}