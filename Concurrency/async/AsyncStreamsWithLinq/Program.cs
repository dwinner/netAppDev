namespace AsyncStreamsWithLinq;

internal static class Program
{
   private static async Task Main()
   {
      var query =
         from i in RangeAsync(0, 10, 500)
         where i % 2 == 0 // Even numbers only.
         select i * 10; // Multiply by 10.

      await foreach (var number in query)
      {
         Console.WriteLine(number);
      }
   }

   private static async IAsyncEnumerable<int> RangeAsync(int start, int count, int delay)
   {
      for (var i = start; i < start + count; i++)
      {
         await Task.Delay(delay).ConfigureAwait(false);
         yield return i;
      }
   }
}