using NetTopologySuite.Geometries;
using RoutesPlanningDomainLayer.Models.BasicTypes;
using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Models.Route;

public interface IRouteOfferRepository : IRepository
{
   RouteOfferAggregate New(Guid id, Coordinate[] path, UserBasicInfo user, DateTime when);
   Task<RouteOfferAggregate?> Get(Guid id);

   Task<IList<RouteOfferAggregate>> GetMatch(
      Point source, Point destination, TimeInterval when, double distance, int maxResults);

   Task DeleteBefore(DateTime milestone);
}