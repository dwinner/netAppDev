var primeNumberTask = Task.Run(() =>
   Enumerable.Range(2, 3_000_000)
      .Count(n => Enumerable.Range(2, (int)Math.Sqrt(n) - 1)
         .All(i => n % i > 0)));

Console.WriteLine("Task running...");
Console.WriteLine($"The answer is {primeNumberTask.Result}");

Task.Factory.StartNew(() =>
{
   Console.WriteLine("Task started");
   Thread.Sleep(2000);
   Console.WriteLine("Foo");
}, TaskCreationOptions.LongRunning);