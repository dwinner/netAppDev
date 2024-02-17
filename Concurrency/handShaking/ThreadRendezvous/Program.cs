namespace ThreadRendezvous;

internal static class Program
{
   //private static object _locker = new();
   private static readonly CountdownEvent Countdown = new(2);

   private static void Main()
   {
      // Get each thread to sleep a random amount of time.
      var r = new Random();
      new Thread(Mate).Start(r.Next(10_000));
      Thread.Sleep(r.Next(10_000));

      Countdown.Signal();
      Countdown.Wait();

      Console.Write("Mate! ");
   }

   private static void Mate(object? delay)
   {
      var del = delay != null ? (int)delay : 0;
      Thread.Sleep(del);
      Countdown.Signal();
      Countdown.Wait();
      Console.Write("Mate! ");
   }
}