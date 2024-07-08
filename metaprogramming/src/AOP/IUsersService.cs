namespace AOP;

public interface IUsersService
{
   Task<Guid> Register(string userName, string password);
}