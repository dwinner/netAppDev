using Grpc.Core;

namespace DualStreamingSvc.Services;

public class MessengerService(ILogger<MessengerService> logger) : Messenger.MessengerBase
{
   private readonly string[] _messages = ["Hi", "Ok", "...", "No words to say", "Bye"];

   public override async Task DataStream(
      IAsyncStreamReader<Request> requestStream,
      IServerStreamWriter<Response> responseStream,
      ServerCallContext context)
   {
      // Get incoming messages in background task
      var readingTask = Task.Run(async () =>
      {
         await foreach (var request in requestStream.ReadAllAsync())
         {
            Console.WriteLine($"Client: {request.Content}");
         }
      });

      foreach (var message in _messages)
      {
         // Send a reply until client closes the stream
         if (!readingTask.IsCompleted)
         {
            await responseStream.WriteAsync(new Response { Content = message });
            Console.WriteLine(message);
            await Task.Delay(TimeSpan.FromSeconds(2));
         }
      }

      // Waiting for reading task to complete
      await readingTask;
   }
}