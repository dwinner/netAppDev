using RoutesPlanningDomainLayer.Models.BasicTypes;

namespace RoutesPlanningDomainLayer.Models.Request;

public interface IRouteRequestState
{
   Guid Id { get; }
   TownBasicInfo Source { get; }
   TownBasicInfo Destination { get; }
   DateTime WhenStart { get; }
   DateTime WhenEnd { get; }
   UserBasicInfo User { get; }
   Guid? RouteId { get; set; }
   public long TimeStamp { get; set; }
}