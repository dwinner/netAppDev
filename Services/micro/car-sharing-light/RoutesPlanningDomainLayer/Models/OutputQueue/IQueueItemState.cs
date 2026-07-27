namespace RoutesPlanningDomainLayer.Models.OutputQueue;

public interface IQueueItemState
{
   Guid Id { get; }
   int MessageCode { get; }
   public string MessageContent { get; }
}