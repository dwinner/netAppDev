using System.Reactive.Linq;

namespace SimpleSelection;

internal static class Program
{
   private static void Main()
   {
      var oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(1));
      var numbersTimesTwo =
         from n in oneNumberPerSecond
         select n * 2;
      Console.WriteLine("Numbers * 2:");
      numbersTimesTwo.Subscribe(Console.WriteLine);

      Console.ReadKey();
   }
}