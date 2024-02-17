namespace TwoWaySignalling_Solution;

internal static class Program
{
   private static readonly object Locker = new();
   private static volatile bool _ready;
   private static volatile bool _go;

   private static void Main()
   {
      new Thread(SaySomething).Start();

      for (var i = 0; i < 5; i++)
      {
         lock (Locker)
         {
            while (!_ready)
            {
               Monitor.Wait(Locker);
            }

            _ready = false;
            _go = true;
            Monitor.PulseAll(Locker);
         }
      }
   }

   private static void SaySomething()
   {
      for (var i = 0; i < 5; i++)
      {
         lock (Locker)
         {
            _ready = true;
            Monitor.PulseAll(Locker); // Remember that calling
            while (!_go)
            {
               Monitor.Wait(Locker); // Monitor.Wait releases
            }

            _go = false; // and reacquires the lock.
            Console.WriteLine("Wassup?");
         }
      }
   }
}