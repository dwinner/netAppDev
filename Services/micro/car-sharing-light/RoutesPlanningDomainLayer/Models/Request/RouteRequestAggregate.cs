using RoutesPlanningDomainLayer.Models.BasicTypes;
using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Models.Request;

public class RouteRequestAggregate(IRouteRequestState state)
   : Entity<Guid>
{
   public override Guid Id => state.Id;
   public TownBasicInfo Source => state.Source;
   public TownBasicInfo Destination => state.Destination;

   public TimeInterval When
   {
      get => field ??= new TimeInterval { Start = state.WhenStart, End = state.WhenEnd };
   } = null!;

   public UserBasicInfo User => state.User;
   public bool Open => state.RouteId == null;
   public long TimeStamp => state.TimeStamp;
   public void DetachFromRoute() => state.RouteId = null;
   public void AttachToRoute(Guid routeId) => state.RouteId = routeId;
}