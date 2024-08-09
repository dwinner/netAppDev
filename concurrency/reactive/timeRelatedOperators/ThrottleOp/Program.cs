using System.Reactive.Linq;

namespace ThrottleOp;

internal static class Program
{
   private static void Main()
   {
      var observable = GenerateAlternatingFastAndSlowEvents().ToObservable().Timestamp();
      var throttled = observable.Throttle(TimeSpan.FromMilliseconds(750));

      using (throttled.Subscribe(x => Console.WriteLine("{0}: {1}", x.Value, x.Timestamp)))
      {
         Console.WriteLine("Press any key to unsubscribe");
         Console.ReadKey();
      }

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();
   }

   // Generates events with interval that alternates between 500ms and 1000ms every 5 events
   private static IEnumerable<int> GenerateAlternatingFastAndSlowEvents()
   {
      var i = 0;
      while (true)
      {
         if (i > 1000)
         {
            yield break;
         }

         yield return i;
         Thread.Sleep(i++ % 10 < 5 ? 500 : 1000);
      }
   }
}