using System.Reactive.Linq;

namespace Transformation;

internal static class Program
{
   private static void Main()
   {
      var oneNumberPerSecond = Observable.Interval(TimeSpan.FromSeconds(1));
      var stringsFromNumbers =
         from n in oneNumberPerSecond
         select new string('*', (int)n);

      Console.WriteLine("Strings from numbers:");
      stringsFromNumbers.Subscribe(Console.WriteLine);

      Console.ReadKey();
   }
}
