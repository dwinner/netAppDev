namespace RxPeriodicAndTimeBasedObservables;

internal class UpdatesWebService
{
   public async Task<IEnumerable<string>> GetUpdatesAsync()
   {
      Console.WriteLine("GetUpdatesAsync was called");
      await Task.Delay(1000).ConfigureAwait(false); //simulate latency

      return Enumerable.Range(1, 5)
         .Select(x => $"An Update {x}, {DateTime.Now.ToLongTimeString()}");
   }
}