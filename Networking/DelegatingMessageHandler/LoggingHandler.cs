namespace DelegatingMessageHandler;

internal class LoggingHandler : DelegatingHandler
{
   public LoggingHandler(HttpMessageHandler nextHandler) => InnerHandler = nextHandler;

   protected override async Task<HttpResponseMessage> SendAsync
      (HttpRequestMessage request, CancellationToken cancellationToken)
   {
      Console.WriteLine($"Requesting: {request.RequestUri}");
      var response = await base.SendAsync(request, cancellationToken)
         .ConfigureAwait(false);
      Console.WriteLine($"Got response: {response.StatusCode}");

      return response;
   }
}