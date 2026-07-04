using Grpc.Core;
using Grpc.Net.Client;
using SvcStreamingClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7053");
var client = new Messenger.MessengerClient(channel);
var serverData = client.ServerDataStream(new Request());
var responseStream = serverData.ResponseStream;
var cancellationToken = CancellationToken.None;

/*while (await responseStream.MoveNext(cancellationToken))
{
   var response = responseStream.Current;
   Console.WriteLine(response.Content);
}*/
await foreach (var response in responseStream.ReadAllAsync(cancellationToken))
{
   Console.WriteLine(response);
}

Console.ReadKey();