namespace RoutesPlanningDomainLayer.Tools;

public class ConcurrencyException(Exception ex) : Exception(ex.Message, ex);