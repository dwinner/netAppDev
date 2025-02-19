namespace ConvOverConf.Structured;

public record UserDetails(
   Guid Id,
   Guid UserId,
   string FirstName,
   string LastName,
   string SocialSecurityNumber);