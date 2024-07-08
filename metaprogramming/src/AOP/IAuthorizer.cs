namespace AOP;

public interface IAuthorizer
{
   bool IsAuthorized(string username, string action);
}