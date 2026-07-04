using Grpc.Core;

namespace ClientStreamingSvc.Services;

public class MessengerService(ILogger<MessengerService> logger) : Messenger.MessengerBase
{
   public override async Task<Response> ClientDataStream(
      IAsyncStreamReader<Request> requestStream,
      ServerCallContext context)
   {
      await foreach (var request in requestStream.ReadAllAsync())
      {
         Console.WriteLine(request.Content);
      }

      logger.LogInformation("All done");
      return new Response { Content = "All done" };
   }
}