using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Events;

public class ReleasedRequestsEvent : IEventNotification
{
   public Guid AbortedRoute { get; set; }
}