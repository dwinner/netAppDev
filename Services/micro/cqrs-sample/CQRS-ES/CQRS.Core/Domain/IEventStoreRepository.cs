using CQRS.Core.Events;

namespace CQRS.Core.Domain;

public interface IEventStoreRepository
{
   Task SaveAsync(EventModel anEventModel);

   Task<List<EventModel>> FindByAggregateId(Guid anAggregateId);

   Task<List<EventModel>> FindAllAsync();
}