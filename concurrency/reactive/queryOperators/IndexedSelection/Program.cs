using System.Reactive.Linq;

namespace IndexedSelection;

internal static class Program
{
   private static void Main()
   {
      var clock = Observable.Interval(TimeSpan.FromSeconds(1))
         .Select((_, index) => new TimeIndex(index, DateTimeOffset.Now));

      clock.Subscribe(timeIndex =>
      {
         Console.WriteLine("Ding dong. The time is now {0:T}. This is event number {1}.",
            timeIndex.Time,
            timeIndex.Index);
      });

      Console.ReadKey();
   }
}