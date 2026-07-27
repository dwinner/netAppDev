using DualStreamingClient;
using Grpc.Core;
using Grpc.Net.Client;

var messages = new[] { "Hi there", "What's up?", "Silent...", "Sleeping?", "Then Bye" };
using var channel = GrpcChannel.ForAddress("http://localhost:5025");
var client = new Messenger.MessengerClient(channel);
var call = client.DataStream();

var readingTask = Task.Run(async () =>
{
   await foreach (var response in call.ResponseStream.ReadAllAsync())
   {
      Console.WriteLine($"From server: {response.Content}");
   }
});

foreach (var message in messages)
{
   await call.RequestStream.WriteAsync(new Request { Content = message });
   Console.WriteLine(message);
   await Task.Delay(TimeSpan.FromSeconds(2));
}

await call.RequestStream.CompleteAsync();
await readingTask;

Console.ReadKey();