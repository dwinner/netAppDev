using System;
using System.Collections.Generic;
using SharedMessages.BasicTypes;

namespace SharedMessages.RouteNegotiation;

public class RouteExtensionProposalsMessage : TimedMessage
{
   public Guid RouteId { get; set; }
   public IList<RouteRequestMessage>? Proposals { get; set; }
}