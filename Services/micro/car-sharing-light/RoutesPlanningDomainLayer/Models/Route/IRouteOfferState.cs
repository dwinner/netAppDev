using NetTopologySuite.Geometries;
using RoutesPlanningDomainLayer.Models.BasicTypes;

namespace RoutesPlanningDomainLayer.Models.Route;

public interface IRouteOfferState
{
   Guid Id { get; }
   LineString Path { get; set; }
   DateTime When { get; }
   UserBasicInfo User { get; }
   RouteStatus Status { get; set; }
   public long TimeStamp { get; set; }
}