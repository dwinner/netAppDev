namespace CustomCombinators;

internal static class Program
{
   private static async Task Main()
   {
      // With timeout support
      try
      {
         var result1 = await SomeFunc1Async().WithTimeoutAsync(TimeSpan.FromSeconds(2))
            .ConfigureAwait(false);
         Console.WriteLine(result1);
      }
      catch (TimeoutException timeoutEx)
      {
         Console.WriteLine(timeoutEx.Message);
      }

      // With cancellation support
      try
      {
         var cts = new CancellationTokenSource(3_000); // Cancel after 3 seconds
         var result2 = await SomeFunc2Async().WithCancellationAsync(cts.Token)
            .ConfigureAwait(false);
         Console.WriteLine(result2);
      }
      catch (TaskCanceledException taskCanceledEx)
      {
         Console.WriteLine(taskCanceledEx.Message);
      }

      // With error control
      var task1 = Task.Run(() =>
      {
         throw null;
         return 42;
      });
      var task2 = Task.Delay(5000).ContinueWith(ant => 53);

      try
      {
         var results = await new[] { task1, task2 }.WhenAllOrErrorAsync()
            .ConfigureAwait(false);
         Array.ForEach(results, Console.WriteLine);
      }
      catch (NullReferenceException nullReferenceEx)
      {
         Console.WriteLine(nullReferenceEx.Message);
      }

      Console.ReadLine();
   }

   private static async Task<string> SomeFunc1Async()
   {
      await Task.Delay(10_000).ConfigureAwait(false);
      return "foo";
   }

   private static async Task<string> SomeFunc2Async()
   {
      await Task.Delay(10_000)
         .ConfigureAwait(false);
      return "foo";
   }
}