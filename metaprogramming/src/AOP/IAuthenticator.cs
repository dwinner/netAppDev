namespace AOP;

public interface IAuthenticator
{
   bool Authenticate(string username, string password);
}