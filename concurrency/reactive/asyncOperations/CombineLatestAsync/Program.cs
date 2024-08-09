using System.Reactive.Linq;

namespace CombineLatestAsync;

internal static class Program
{
   private static void Main()
   {
      ParallelExecutionTest();
      Console.ReadKey();
   }

   private static async void ParallelExecutionTest()
   {
      var o = Observable.CombineLatest(
         Observable.Start(() =>
         {
            Console.WriteLine("Executing 1st on Thread: {0}", Environment.CurrentManagedThreadId);
            return "Result A";
         }),
         Observable.Start(() =>
         {
            Console.WriteLine("Executing 2nd on Thread: {0}", Environment.CurrentManagedThreadId);
            return "Result B";
         }),
         Observable.Start(() =>
         {
            Console.WriteLine("Executing 3rd on Thread: {0}", Environment.CurrentManagedThreadId);
            return "Result C";
         })
      ).Finally(() => Console.WriteLine("Done!"));

      foreach (var r in await o.FirstAsync())
      {
         Console.WriteLine(r);
      }
   }
}