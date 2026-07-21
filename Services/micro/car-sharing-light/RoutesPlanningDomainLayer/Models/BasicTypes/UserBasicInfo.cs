namespace RoutesPlanningDomainLayer.Models.BasicTypes;

public record UserBasicInfo
{
   public Guid Id { get; init; }
   public string DisplayName { get; init; } = null!;
}