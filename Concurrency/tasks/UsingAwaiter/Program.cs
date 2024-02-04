var primeNumberTask = Task.Run(() =>
   Enumerable.Range(2, 3_000_000)
      .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1)
         .All(i => n % i > 0)));

var awaiter = primeNumberTask.GetAwaiter();
awaiter.OnCompleted(() =>
{
   var result = awaiter.GetResult();
   Console.WriteLine(result); // Writes result
});

Console.ReadLine();