using System.Collections.Generic;
using SharedMessages.BasicTypes;

namespace SharedMessages.RouteNegotiation;

public class RouteExtendedMessage : TimedMessage
{
   public RouteOfferMessage? ExtendedRoute { get; set; }
   public IList<RouteRequestMessage>? AddedRequests { get; set; }
   public bool Closed { get; set; }
}