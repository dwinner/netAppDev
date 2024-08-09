using System.Reactive.Linq;

namespace SimpleInterval;

internal static class Program
{
   private static void Main()
   {
      var observable = Observable.Interval(TimeSpan.FromSeconds(1));

      using (observable.Subscribe(Console.WriteLine))
      {
         Console.WriteLine("Press any key to unsubscribe");
         Console.ReadKey();
      }

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();
   }
}