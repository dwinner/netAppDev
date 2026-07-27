using Grpc.Net.Client;
using SvcStreamingClient2;

var messages = new[]
{
   "Hi there",
   "What's up?",
   "Keep silence",
   "Sleeping?",
   "Then bye"
};

using var channel = GrpcChannel.ForAddress("http://localhost:5100");
var client = new Messenger.MessengerClient(channel);
var call = client.ClientDataStream();
foreach (var message in messages)
{
   await call.RequestStream.WriteAsync(new Request { Content = message });
}

await call.RequestStream.CompleteAsync();
var response = await call.ResponseAsync;
Console.WriteLine($"Server's answer: {response.Content}");

Console.ReadKey();