namespace WaitPulseSignalling;

internal static class Program
{
   private static readonly object Locker = new();
   private static bool _go;

   private static void Main()
   {
      // The new thread will block because _go==false.
      new Thread(Work).Start();
      Console.WriteLine("Press Enter to signal");
      Console.ReadLine(); // Wait for user to hit Enter

      lock (Locker) // Let's now wake up the thread by
      { // setting _go=true and pulsing.
         _go = true;
         Monitor.Pulse(Locker);
      }
   }

   private static void Work()
   {
      lock (Locker)
      {
         while (!_go)
         {
            Monitor.Wait(Locker); // Lock is released while we’re waiting
         }
      }

      Console.WriteLine("Woken!!!");
   }
}