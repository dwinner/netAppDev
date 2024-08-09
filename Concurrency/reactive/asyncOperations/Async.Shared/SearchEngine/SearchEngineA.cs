namespace Async.Shared.SearchEngine;

internal class SearchEngineA : ISearchEngine
{
   public async Task<IEnumerable<string>> SearchAsync(string term)
   {
      Console.WriteLine("SearchEngine A - SearchAsync()");

      await Task.Delay(1500).ConfigureAwait(false); //simulate latency
      return new[] { "resultA", "resultB" };
   }
}