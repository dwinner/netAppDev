DisplayPrimeCounts();

return;

void DisplayPrimeCounts()
{
   DisplayPrimeCountsFrom(0);
}

void DisplayPrimeCountsFrom(int i) // This is starting to get awkward!
{
   var awaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
   awaiter.OnCompleted(() =>
   {
      var result = awaiter.GetResult();
      Console.WriteLine($"{result} primes between {i * 1000000} and {((i + 1) * 1000000 - 1)}");
      if (i++ < 10)
      {
         DisplayPrimeCountsFrom(i);
      }
      else
      {
         Console.WriteLine("Done");
      }
   });
}

Task<int> GetPrimesCountAsync(int start, int count) =>
   Task.Run(() =>
      ParallelEnumerable.Range(start, count).Count(n =>
         Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));