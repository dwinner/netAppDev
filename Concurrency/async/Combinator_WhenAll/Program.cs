namespace Combinator_WhenAll;

internal static class Program
{
   private static async Task Main()
   {
      var totalSize =
         await GetTotalSizeAsync("http://www.linqpad.net http://www.albahari.com http://stackoverflow.com".Split())
            .ConfigureAwait(false);
      Console.WriteLine(totalSize);
   }

   private static async Task<int> GetTotalSizeAsync(IEnumerable<string> uris)
   {
      IEnumerable<Task<int>> downloadTasks = uris.Select(async uri =>
         (await new HttpClient().GetStringAsync(uri).ConfigureAwait(false)).Length);

      var contentLengths = await Task.WhenAll(downloadTasks).ConfigureAwait(false);
      return contentLengths.Sum();
   }
}