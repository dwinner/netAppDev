namespace RxAsyncObservables.SearchEngine;

internal class SearchEngineB : ISearchEngine
{
   public async Task<IEnumerable<string>> SearchAsync(string term)
   {
      Console.WriteLine("SearchEngine B - SearchAsync()");
      await Task.Delay(1500).ConfigureAwait(false); //simulate latency
      return new[] { "resultC", "resultD" }.AsEnumerable();
   }
}