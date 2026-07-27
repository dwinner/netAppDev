using System;
using SharedMessages.BasicTypes;

namespace SharedMessages.RouteNegotiation;

public class RouteClosedAbortedMessage : TimedMessage
{
   public Guid RouteId { get; set; }
   public bool IsAborted { get; set; }
}