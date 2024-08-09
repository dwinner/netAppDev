using System.Reactive.Linq;

namespace SampleOp;

internal static class Program
{
   private static void Main()
   {
      // Generate sequence of numbers, (an interval of 50 ms seems to result in approx 16 per second).
      var observable = Observable.Interval(TimeSpan.FromMilliseconds(50));

      // Sample the sequence every second
      using (observable.Sample(TimeSpan.FromSeconds(1)).Timestamp().Subscribe(
                x => Console.WriteLine("{0}: {1}", x.Value, x.Timestamp)))
      {
         Console.WriteLine("Press any key to unsubscribe");
         Console.ReadKey();
      }

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();
   }
}