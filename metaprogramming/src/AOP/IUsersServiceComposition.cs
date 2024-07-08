namespace AOP;

public interface IUsersServiceComposition : IUsersService, IAuthenticator, IAuthorizer;