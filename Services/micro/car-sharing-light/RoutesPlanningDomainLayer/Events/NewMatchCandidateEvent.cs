using RoutesPlanningDomainLayer.Tools;

namespace RoutesPlanningDomainLayer.Events;

public record NewMatchCandidateEvent<T>(T MatchCandidate) : IEventNotification;