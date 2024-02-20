// Calculate prime numbers with ordered output.

var numbers = Enumerable.Range(3, 1_000_000 - 3);

var parallelQuery =
   from n in numbers.AsParallel() /*.AsOrdered()*/
   where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
   select n;

var primes = parallelQuery.ToArray();
foreach (var prime in primes.Take(100))
{
   Console.Write($"{prime} ");
}

// In this example, we could alternatively call OrderBy at the end of the query.