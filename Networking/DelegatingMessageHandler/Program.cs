using System.Net;

namespace DelegatingMessageHandler;

internal static class Program
{
   private static async Task Main()
   {
      var mocker = new MockHandler(request =>
         new HttpResponseMessage(HttpStatusCode.OK)
         {
            Content = new StringContent($"You asked for {request.RequestUri}")
         });

      var logger = new LoggingHandler(mocker);
      var client = new HttpClient(logger);
      var response = await client.GetAsync("http://www.linqpad.net")
         .ConfigureAwait(false);
      var result = await response.Content.ReadAsStringAsync()
         .ConfigureAwait(false);

      Assert.AreEqual("You asked for http://www.linqpad.net/", result);
   }
}