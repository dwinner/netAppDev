using Fundamentals;
using JetBrains.Annotations;

namespace ConvOverConf.Structured;

[Singleton]
[UsedImplicitly]
public class UserDetailsService(IDatabase database) : IUserDetailsService
{
   public Task Register(string firstName, string lastName, string socialSecurityNumber, Guid userId)
      => database
         .GetCollectionFor<UserDetails>()
         .InsertOneAsync(
            new UserDetails(Guid.NewGuid(), userId, firstName, lastName, socialSecurityNumber));
}