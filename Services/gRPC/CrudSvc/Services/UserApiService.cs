using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CrudSvc.Services;

public class UserApiService(ILogger<UserApiService> logger) : UserService.UserServiceBase
{
   private static int _Id;

   private static readonly List<User> _Users =
   [
      new(++_Id, "Tom", 38),
      new(++_Id, "Bob", 42)
   ];

   public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
   {
      logger.LogInformation(nameof(ListUsers));
      var listReply = new ListReply();
      var userList = _Users.Select(user => new UserReply { Id = user.Id, Name = user.Name, Age = user.Age }).ToList();
      listReply.Users.AddRange(userList);

      return Task.FromResult(listReply);
   }

   public override Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
   {
      var user = _Users.Find(user => user.Id == request.Id);
      if (user is null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      var userReply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };
      return Task.FromResult(userReply);
   }

   public override Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
   {
      var user = new User(++_Id, request.Name, request.Age);
      _Users.Add(user);
      return Task.FromResult(new UserReply { Id = user.Id, Name = user.Name, Age = user.Age });
   }

   public override Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
   {
      var user = _Users.Find(user => user.Id == request.Id);
      if (user is null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      user = user with { Name = request.Name };
      user = user with { Age = request.Age };
      var reply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return Task.FromResult(reply);
   }

   public override Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
   {
      var user = _Users.Find(user => user.Id == request.Id);
      if (user is null)
      {
         throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
      }

      _Users.Remove(user);
      var reply = new UserReply { Id = user.Id, Name = user.Name, Age = user.Age };

      return Task.FromResult(reply);
   }
}