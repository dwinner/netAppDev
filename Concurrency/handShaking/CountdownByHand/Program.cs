namespace CountdownByHand;

internal static class Program
{
   private static void Main()
   {
      var cd = new Countdown(5);
      new Thread(() =>
      {
         for (var i = 0; i < 5; i++)
         {
            Thread.Sleep(1000);
            cd.Signal();
            Console.WriteLine($"Signal {i}");
         }
      }).Start();
      Console.WriteLine("Waiting");
      cd.Wait();
      Console.WriteLine("Unblocked");
   }
}