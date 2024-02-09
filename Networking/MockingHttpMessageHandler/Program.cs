using System.Net;

namespace MockingHttpMessageHandler;

internal static class Program
{
   private static async Task Main()
   {
      var mocker = new MockHandler(request =>
         new HttpResponseMessage(HttpStatusCode.OK)
         {
            Content = new StringContent($"You asked for {request.RequestUri}")
         });

      var client = new HttpClient(mocker);
      var response = await client.GetAsync("http://www.linqpad.net")
         .ConfigureAwait(false);
      var result = await response.Content.ReadAsStringAsync()
         .ConfigureAwait(false);

      Console.WriteLine(result);
   }
}