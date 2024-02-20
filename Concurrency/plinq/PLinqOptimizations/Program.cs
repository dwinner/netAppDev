using System.Collections.Concurrent;

// output-side optimization
"abcdef".AsParallel().Select(char.ToUpper).ForAll(Console.Write);

Console.WriteLine();

// input-side optimization: forcing chunk partitioning
int[] numbers = { 3, 4, 5, 6, 7, 8, 9 };

var parallelQuery =
   Partitioner.Create(numbers, true).AsParallel()
      .Where(n => n % 2 == 0);

parallelQuery.ForAll(word => { Console.Write("{0} ", word); });