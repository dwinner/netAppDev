using System.Reactive.Linq;
using RxHelpers;

namespace RxDealingWithBackpressure;

internal static class Program
{
   private static void Main()
   {
      BackpressureExample();
      LossyBackpressureHandlingUsingCombineLatest();
      Console.ReadLine();
   }

   private static void LossyBackpressureHandlingUsingCombineLatest()
   {
      Demo.DisplayHeader("Backpressure example - using CombineLatest to drop old notifications (lossy approach)");

      var heartRatesValues = new[] { 70, 75, 80, 90, 80 };
      var speedValues = new[] { 50, 51, 53, 52, 55 };

      var heartRates = Observable.Interval(TimeSpan.FromSeconds(1))
         .Select(x => heartRatesValues[x % heartRatesValues.Length]);
      var speeds = Observable.Interval(TimeSpan.FromSeconds(3)).Select(x => speedValues[x % speedValues.Length]);

      heartRates.CombineLatest(speeds, (h, s) => $"Heart:{h} Speed:{s}")
         .Take(5)
         .SubscribeConsole();
   }

   private static void BackpressureExample()
   {
      Demo.DisplayHeader("Backpressure example - Zipping a fast observable with a slow observable");
      Console.WriteLine("Press <enter> to stop the example");

      //Zipping a fast observable with a slow observable will have to store
      //the elements from the fast observable in memory
      var fast = Observable.Interval(TimeSpan.FromSeconds(1));
      var slow = Observable.Interval(TimeSpan.FromSeconds(2));

      var zipped = slow.Zip(fast, (x, y) => x);
      var subscription =
         zipped
            .Select(x => $"{x} elements are in memory")
            .SubscribeConsole("Backpressure");

      Console.ReadLine();
      subscription.Dispose();
   }
}