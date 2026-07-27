using NetTopologySuite.Geometries;

namespace RoutesPlanningDomainLayer.Models.BasicTypes;

public record TownBasicInfo
{
   public Guid Id { get; init; }
   public string Name { get; init; } = null!;
   public Point Location { get; init; } = null!;
}