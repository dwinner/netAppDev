using Grpc.Core;
using Grpc.Net.Client;
using HeadersClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5139");
var client = new Messenger.MessengerClient(channel);

var requestHeaders = new Metadata
{
   { "username", "Tom" }
};
var call = client.SendMessageAsync(new Request(), requestHeaders);

var response = await call;
Console.WriteLine($"Response: {response.Content}");
var headers = await call.ResponseHeadersAsync;
foreach (var header in headers)
{
   Console.WriteLine($"{header.Key}: {header.Value}");
}

Console.ReadKey();