var million = Enumerable.Range(3, 1_000_000);

var cancelSource = new CancellationTokenSource();

var primeNumberQuery =
   from n in million.AsParallel().WithCancellation(cancelSource.Token)
   where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
   select n;

new Thread(() =>
   {
      Thread.Sleep(100); // Cancel query after
      cancelSource.Cancel(); // 100 milliseconds.
   }
).Start();

try
{
   // Start query running:
   var primes = primeNumberQuery.ToArray();
   // We'll never get here because the other thread will cancel us.
}
catch (OperationCanceledException)
{
   Console.WriteLine("Query canceled");
}