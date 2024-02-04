using Timer = System.Timers.Timer;

var awaiter = GetAnswerToLife().GetAwaiter();
awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult()));

Console.ReadLine();

return;

Task<int> GetAnswerToLife()
{
   var tcs = new TaskCompletionSource<int>();

   // Create a timer that fires once in 5000 ms:
   var timer = new Timer(5000) { AutoReset = false };
   timer.Elapsed += (_, e) =>
   {
      Console.WriteLine(e.SignalTime);
      timer.Dispose();
      tcs.SetResult(42);
   };

   timer.Start();
   return tcs.Task;
}