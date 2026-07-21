using System;
using SharedMessages.BasicTypes;

namespace SharedMessages.RouteNegotiation;

public class RouteRequestMessage : TimedMessage
{
   public Guid Id { get; set; }
   public TownBasicInfoMessage? Source { get; set; }
   public TownBasicInfoMessage? Destination { get; set; }
   public TimeIntervalMessage? When { get; set; }
   public UserBasicInfoMessage? User { get; set; }
}