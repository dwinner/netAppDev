using EfCrudClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5122");
var client = new UserService.UserServiceClient(channel);

// get the list
var users = await client.ListUsersAsync(new Empty());
foreach (var user in users.Users)
{
   Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
}

// get one
try
{
   var getUser = await client.GetUserAsync(new GetUserRequest { Id = 2 });
   Console.WriteLine($"{getUser.Id}. {getUser.Name} - {getUser.Age}");
}
catch (RpcException ex)
{
   Console.WriteLine(ex.Status.Detail);
}

// create
var createdUser = await client.CreateUserAsync(new CreateUserRequest { Name = "Alice", Age = 32 });
Console.WriteLine($"{createdUser.Id}. {createdUser.Name} - {createdUser.Age}");

// update
try
{
   var updateUser = await client.UpdateUserAsync(new UpdateUserRequest { Id = 1, Name = "Tomas", Age = 38 });
   Console.WriteLine($"{updateUser.Id}. {updateUser.Name} - {updateUser.Age}");
}
catch (RpcException ex)
{
   Console.WriteLine(ex.Status.Detail);
}

// delete
try
{
   var delUser = await client.DeleteUserAsync(new DeleteUserRequest { Id = 2 });
   Console.WriteLine($"{delUser.Id}. {delUser.Name} - {delUser.Age}");
}
catch (RpcException ex)
{
   Console.WriteLine(ex.Status.Detail);
}

Console.ReadKey();