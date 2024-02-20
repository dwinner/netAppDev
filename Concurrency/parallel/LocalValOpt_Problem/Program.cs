var locker = new object();
double total = 0;
Parallel.For(1, 10_000_000,
   i =>
   {
      lock (locker)
      {
         total += Math.Sqrt(i);
      }
   });
Console.WriteLine(total);