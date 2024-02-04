await DisplayPrimeCountsAsync().ConfigureAwait(false);
return;

async Task DisplayPrimeCountsAsync()
{
   for (var i = 0; i < 10; i++)
   {
      var result = await GetPrimesCountAsync(i * 1_000_000 + 2, 1_000_000)
         .ConfigureAwait(false);
      Console.WriteLine($"{result} primes between {i * 1_000_000} and {(i + 1) * 1_000_000 - 1}");
   }

   Console.WriteLine("Done!");
}

Task<int> GetPrimesCountAsync(int start, int count) =>
   Task.Run(() =>
      ParallelEnumerable.Range(start, count).Count(n =>
         Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));