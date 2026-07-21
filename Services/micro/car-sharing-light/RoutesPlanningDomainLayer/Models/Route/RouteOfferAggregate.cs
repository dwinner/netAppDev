using System.Collections.Immutable;
using NetTopologySuite.Geometries;
using RoutesPlanningDomainLayer.Events;
using RoutesPlanningDomainLayer.Models.BasicTypes;
using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Models.Route;

public class RouteOfferAggregate(IRouteOfferState state) : Entity<Guid>
{
   private IReadOnlyList<Coordinate>? _path;
   public override Guid Id => state.Id;
   public IReadOnlyList<Coordinate> Path => _path ??= state.Path.Coordinates.ToImmutableList();
   public DateTime When => state.When;
   public UserBasicInfo User => state.User;
   public RouteStatus Status => state.Status;
   public long TimeStamp => state.TimeStamp;

   public void Extend(long timestamp, IEnumerable<Guid> addedRequests, Coordinate[] newRoute, bool closed)
   {
      if (timestamp > TimeStamp)
      {
         state.Path = new LineString(newRoute)
            { SRID = GeometryConstants.DefaultSrid };
         _path = null;
         state.TimeStamp = timestamp;
      }

      if (state.Status != RouteStatus.Aborted)
      {
         AddDomainEvent(new AttachedRequestEvent
         {
            AddedRequests = addedRequests,
            RouteOffer = Id
         });
      }

      Close();
   }

   public void Close()
   {
      state.Status = RouteStatus.Closed;
   }

   public void Abort()
   {
      state.Status = RouteStatus.Aborted;
      AddDomainEvent(new ReleasedRequestsEvent
      {
         AbortedRoute = Id
      });
   }
}