namespace RoutesPlanningDomainLayer.Tools;

public class ConstraintViolationException(Exception ex) : Exception(ex.Message, ex);