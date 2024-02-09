namespace DelegatingMessageHandler;

internal class MockHandler(Func<HttpRequestMessage, HttpResponseMessage> responseGenerator)
   : HttpMessageHandler
{
   protected override Task<HttpResponseMessage> SendAsync
      (HttpRequestMessage request, CancellationToken cancellationToken)
   {
      cancellationToken.ThrowIfCancellationRequested();
      var response = responseGenerator(request);
      response.RequestMessage = request;
      return Task.FromResult(response);
   }
}