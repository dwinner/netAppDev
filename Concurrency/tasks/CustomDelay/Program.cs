using Timer = System.Timers.Timer;

for (var i = 0; i < 10_000; i++)
{
   Delay(5_000).GetAwaiter().OnCompleted(() => Console.WriteLine(42));
}

Console.ReadKey();

return;

Task Delay(int milliseconds)
{
   var tcs = new TaskCompletionSource<object?>();
   var timer = new Timer(milliseconds) { AutoReset = false };
   timer.Elapsed += (s, e) =>
   {
      timer.Dispose();
      tcs.SetResult(null);
   };

   timer.Start();
   return tcs.Task;
}