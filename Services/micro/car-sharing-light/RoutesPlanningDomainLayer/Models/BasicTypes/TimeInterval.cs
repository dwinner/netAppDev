namespace RoutesPlanningDomainLayer.Models.BasicTypes;

public record TimeInterval
{
   public DateTime Start { get; init; }
   public DateTime End { get; init; }
}