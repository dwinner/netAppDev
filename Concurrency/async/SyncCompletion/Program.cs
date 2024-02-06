namespace SyncCompletion;

internal static class Program
{
   private static readonly Dictionary<string, string> Cache = new();

   public static async Task Main()
   {
      var html = await GetWebPageAsync("http://www.linqpad.net")
         .ConfigureAwait(false);
      Console.WriteLine(html.Length);

      // Let's try again. It should be instant this time:
      html = await GetWebPageAsync("http://www.linqpad.net")
         .ConfigureAwait(false);
      Console.WriteLine(html.Length);
   }

   private static async Task<string> GetWebPageAsync(string uri)
   {
      if (Cache.TryGetValue(uri, out var html))
      {
         return html;
      }

      return Cache[uri] = await new HttpClient().GetStringAsync(uri)
         .ConfigureAwait(false);
   }
}