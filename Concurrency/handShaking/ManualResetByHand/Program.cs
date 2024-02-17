namespace ManualResetByHand;

internal static class Program
{
   private static readonly object Locker = new();
   private static volatile bool _signal;

   private static void Main()
   {
      new Thread(() =>
      {
         Thread.Sleep(2000);
         Set();
      }).Start();
      Console.WriteLine("Waiting...");
      WaitOne();
      Console.WriteLine("Signaled");
   }

   private static void WaitOne()
   {
      lock (Locker)
      {
         while (!_signal)
         {
            Monitor.Wait(Locker);
         }
      }
   }

   private static void Set()
   {
      lock (Locker)
      {
         _signal = true;
         Monitor.PulseAll(Locker);
      }
   }

   private static void Reset()
   {
      lock (Locker)
      {
         _signal = false;
      }
   }
}