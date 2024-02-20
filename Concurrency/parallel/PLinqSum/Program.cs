var sum = ParallelEnumerable
   .Range(1, 10_000_000)
   .Sum(i => Math.Sqrt(i));
Console.WriteLine(sum);