using System.Reactive.Linq;

namespace SimpleDelay;

internal static class Program
{
   private static void Main()
   {
      var oneNumberEveryFiveSeconds = Observable.Interval(TimeSpan.FromSeconds(5));

      // Instant echo
      oneNumberEveryFiveSeconds.Subscribe(Console.WriteLine);

      // One-second delay
      oneNumberEveryFiveSeconds.Delay(TimeSpan.FromSeconds(1))
         .Subscribe(num => { Console.WriteLine("...{0}...", num); });

      // Two-second delay
      oneNumberEveryFiveSeconds.Delay(TimeSpan.FromSeconds(2)).Subscribe(num =>
      {
         Console.WriteLine("......{0}......", num);
      });

      Console.ReadKey();
   }
}