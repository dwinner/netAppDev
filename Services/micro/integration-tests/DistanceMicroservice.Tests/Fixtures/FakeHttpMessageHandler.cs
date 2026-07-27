namespace DistanceMicroservice.Tests.Fixtures;

public class FakeHttpMessageHandler(HttpResponseMessage fakeResponse) : HttpMessageHandler
{
   protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
      CancellationToken cancellationToken) => Task.FromResult(fakeResponse);
}