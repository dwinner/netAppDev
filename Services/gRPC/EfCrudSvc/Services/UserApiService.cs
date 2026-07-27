using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace EfCrudSvc.Services;

public class UserApiService(ILogger<UserApiService> logger, ApplicationContext appCtx) : UserService.UserServiceBase
{
   public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
   {
      logger.LogInformation(nameof(ListUsers));

      var listReply = new ListReply();
      var userList = appCtx.Users.Select(item => new UserReply { Id = item.Id, Name = item.Name, Age = item.Age })
         .ToList();
      listReply.Users.AddRange(userList);

      return Task.FromResult(listReply);
   }

   public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
   {
      logger.LogInformation(nameof(GetUser));

      var user = await appCtx.Users.FindAsync(request.Id);
      if (user == null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      var userReply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return await Task.FromResult(userReply);
   }

   public override async Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
   {
      var user = new User { Name = request.Name, Age = request.Age };
      await appCtx.Users.AddAsync(user);
      await appCtx.SaveChangesAsync();
      var reply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return await Task.FromResult(reply);
   }

   public override async Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
   {
      var user = await appCtx.Users.FindAsync(request.Id);
      if (user == null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      user.Name = request.Name;
      user.Age = request.Age;
      await appCtx.SaveChangesAsync();
      var reply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return await Task.FromResult(reply);
   }

   public override async Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
   {
      var user = await appCtx.Users.FindAsync(request.Id);
      if (user == null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      appCtx.Users.Remove(user);
      await appCtx.SaveChangesAsync();
      var reply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return await Task.FromResult(reply);
   }
}