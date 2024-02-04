await DisplayPrimeCountsAsync().ConfigureAwait(false);
return;

static Task DisplayPrimeCountsAsync()
{
   var machine = new PrimesStateMachine();
   machine.DisplayPrimeCountsFrom(0);
   return machine.Task;
}

internal class PrimesStateMachine // Even more awkward!!
{
   private readonly TaskCompletionSource<object?> _tcs = new();
   public Task Task => _tcs.Task;

   public void DisplayPrimeCountsFrom(int i)
   {
      var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
      awaiter.OnCompleted(() =>
      {
         Console.WriteLine(awaiter.GetResult());
         if (i++ < 10)
         {
            DisplayPrimeCountsFrom(i);
         }
         else
         {
            Console.WriteLine("Done");
            _tcs.SetResult(null);
         }
      });
   }

   private static Task<int> GetPrimesCountAsync(int start, int count) =>
      Task.Run(() =>
         ParallelEnumerable.Range(start, count).Count(n =>
            Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
}