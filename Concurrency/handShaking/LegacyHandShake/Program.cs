namespace LegacyHandShake;

internal static class Program
{
   // Non-deterministic!

   private static readonly object Locker = new();

   private static void Main()
   {
      new Thread(Work).Start();
      Thread.Sleep(TimeSpan.FromSeconds(2));

      lock (Locker)
      {
         Monitor.Pulse(Locker);
      }
   }

   private static void Work()
   {
      lock (Locker)
      {
         Monitor.Wait(Locker);
      }

      Console.WriteLine("Woken!!!");
   }
}