namespace TwoWaySignalling;

internal static class Program
{
   private static readonly object Locker = new();
   private static bool _go;

   private static void Main()
   {
      new Thread(SaySomething).Start();

      for (var i = 0; i < 5; i++)
      {
         lock (Locker)
         {
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
            while (!_go)
            {
               Monitor.Wait(Locker);
            }

            _go = false;
            Console.WriteLine("Wassup?");
         }
      }
   }
}