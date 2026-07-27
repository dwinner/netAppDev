using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Events;

public class AttachedRequestEvent : IEventNotification
{
   public IEnumerable<Guid> AddedRequests { get; set; } = new List<Guid>();
   public Guid RouteOffer { get; set; }
}