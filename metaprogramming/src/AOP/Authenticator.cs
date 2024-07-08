namespace AOP;

public class Authenticator : IAuthenticator
{
   public bool Authenticate(string username, string password) => true;
}