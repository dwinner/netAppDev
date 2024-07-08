using Microsoft.Extensions.Logging;

namespace AOP;

public class UsersService(ILogger<UsersService> logger) : IUsersService
{
   public Task<Guid> Register(string userName, string password)
   {
      logger.LogInformation("Inside register method");
      var id = Guid.NewGuid();
      return Task.FromResult(id);
   }
}