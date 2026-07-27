using System.Text.Json;
using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Models.OutputQueue;

public class QueueItem(IQueueItemState state) : Entity<Guid>
{
   public override Guid Id => state.Id;
   public int MessageCode => state.MessageCode;

   public T? GetMessage<T>()
   {
      if (string.IsNullOrWhiteSpace(state.MessageContent))
      {
         return default;
      }

      return JsonSerializer.Deserialize<T>(state.MessageContent);
   }
}