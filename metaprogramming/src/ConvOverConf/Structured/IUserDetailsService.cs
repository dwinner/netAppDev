namespace ConvOverConf.Structured;

public interface IUserDetailsService
{
   Task Register(string firstName, string lastName, string socialSecurityNumber, Guid userId);
}