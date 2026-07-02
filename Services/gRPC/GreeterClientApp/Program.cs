using GreeterClientApp;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("https://localhost:7010");
var client = new Greeter.GreeterClient(channel);
Console.Write("Enter the name: ");
var name = Console.ReadLine();
var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Server answer: {reply.Message}");
Console.ReadKey();