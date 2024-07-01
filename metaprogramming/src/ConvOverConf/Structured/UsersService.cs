using Fundamentals;
using JetBrains.Annotations;

namespace ConvOverConf.Structured;

[Singleton]
[UsedImplicitly]
public class UsersService(IDatabase database) : IUsersService
{
   public async Task<Guid> Register(string userName, string password)
   {
      var user = new User(Guid.NewGuid(), userName, password);
      await database.GetCollectionFor<User>().InsertOneAsync(user);
      return user.Id;
   }
}