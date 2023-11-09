using System.Reactive.Linq;

namespace IntroducingRx;

internal static class Program
{
   private static void Main()
   {
      var strings = new[] { "Hello", "Rx", "A", "AB" }.ToObservable();
      using var _ = strings
         .Where(str => str.StartsWith("A"))
         .Select(str => str.ToUpper())
         .Subscribe(Console.WriteLine);
      //subscription.Dispose();
   }
}