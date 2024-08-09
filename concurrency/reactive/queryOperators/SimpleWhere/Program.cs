using System.Reactive.Linq;

namespace SimpleWhere;

internal static class Program
{
   private static void Main()
   {
      var oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(1));
      var lowNums = from n in oneNumberPerSecond
         where n < 5
         select n;

      Console.WriteLine("Numbers < 5:");
      lowNums.Subscribe(Console.WriteLine);

      Console.ReadKey();
   }
}