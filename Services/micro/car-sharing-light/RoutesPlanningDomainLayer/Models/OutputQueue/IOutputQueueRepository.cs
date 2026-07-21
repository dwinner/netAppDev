using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Models.OutputQueue;

public interface IOutputQueueRepository : IRepository
{
   Task<IList<QueueItem>> Take(int n, TimeSpan requeueAfter);
   void Confirm(Guid[] ids);
   QueueItem New<T>(T item, int messageCode);
}