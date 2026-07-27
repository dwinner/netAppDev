using System.Data;

namespace RoutesPlanningDomainLayer.Tools;

public interface IUnitOfWork
{
   Task<bool> SaveEntitiesAsync();
   Task StartAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
   Task CommitAsync();
   Task RollbackAsync();
}