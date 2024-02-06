namespace CachingTasks;

internal static class Program
{
   private static readonly Dictionary<string, Task<string>> Cache = new();

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

   private static Task<string> GetWebPageAsync(string uri)
   {
      lock (Cache)
      {
         if (Cache.TryGetValue(uri, out Task<string> downloadTask))
         {
            return downloadTask;
         }

         return Cache[uri] = new HttpClient().GetStringAsync(uri);
      }
   }
}