using Grpc.Core;

namespace ServerStreamingSvc.Services;

public class MessengerService(ILogger<MessengerService> logger) : Messenger.MessengerBase
{
   private readonly string[] _messages =
      ["Hi", "What's up?", "Don't be silent", "Hey, are you sleeping?", "Well, then bye"];

   public override async Task ServerDataStream(
      Request request,
      IServerStreamWriter<Response> responseStream,
      ServerCallContext context)
   {
      logger.LogInformation("Starting...");
      foreach (var message in _messages)
      {
         await responseStream.WriteAsync(new Response { Content = message });
         await Task.Delay(TimeSpan.FromSeconds(1));
      }
   }
}