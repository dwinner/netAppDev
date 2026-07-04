using System.Diagnostics;
using CrudClient;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5108");
var client = new UserService.UserServiceClient(channel);

// Get all
var users = await client.ListUsersAsync(new Empty());
foreach (var userReply in users.Users)
{
   Output(userReply);
}

// Get one
try
{
   var getReply = await client.GetUserAsync(new GetUserRequest { Id = 1 });
   Output(getReply);
}
catch (RpcException rpcEx)
{
   Debug.WriteLine(rpcEx);
}

// Create one
var createReply = await client.CreateUserAsync(new CreateUserRequest { Name = "Sam", Age = 28 });
Output(createReply);

// Update one
try
{
   var updateReply = await client.UpdateUserAsync(new UpdateUserRequest { Id = 1, Name = "Tomas", Age = 38 });
   Output(updateReply);
}
catch (RpcException rpcEx)
{
   Debug.WriteLine(rpcEx);
}

// Delete one
try
{
   var deleteReply = await client.DeleteUserAsync(new DeleteUserRequest { Id = 2 });
   Output(deleteReply);
}
catch (RpcException rpcEx)
{
   Debug.WriteLine(rpcEx);
}

Console.ReadKey();
return;

void Output(UserReply aUserReply)
{
   Console.WriteLine($"{aUserReply.Id}. {aUserReply.Name} - {aUserReply.Age}");
}