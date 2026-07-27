using Grpc.Core;

namespace HeadersSvc.Services;

public class MessengerService(ILogger<MessengerService> logger) : Messenger.MessengerBase
{
   public override async Task<Response> SendMessage(Request request, ServerCallContext context)
   {
      foreach (var header in context.RequestHeaders)
      {
         Console.WriteLine($"{header.Key}: {header.Value}");
      }

      var userName = context.RequestHeaders.GetValue("username") ?? "Undefined";
      var responseHeaders = new Metadata
      {
         { "secret-code", "12345" }
      };
      await context.WriteResponseHeadersAsync(responseHeaders);

      return new Response { Content = $"Hello {userName}" };
   }
}