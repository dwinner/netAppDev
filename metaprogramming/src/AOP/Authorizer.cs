namespace AOP;

public class Authorizer : IAuthorizer
{
   public bool IsAuthorized(string username, string action) => false;
}