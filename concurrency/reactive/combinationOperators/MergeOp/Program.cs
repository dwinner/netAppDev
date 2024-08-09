using System.Reactive.Linq;

namespace MergeOp;

internal static class Program
{
   private static IObservable<int> Xs => Generate(0, new List<int> { 1, 2, 2, 2, 2 });

   private static IObservable<int> Ys => Generate(100, new List<int> { 2, 2, 2, 2, 2 });

   public static void Main(string[] args)
   {
      Console.WriteLine("Press any key to unsubscribe");

      using (Xs.Merge(Ys).Timestamp().Subscribe(
                z => Console.WriteLine("{0,3}: {1}", z.Value, z.Timestamp),
                () => Console.WriteLine("Completed, press a key")))
      {
         Console.ReadKey();
      }

      Console.WriteLine("Press any key to exit");
      Console.ReadKey();
   }

   private static IObservable<int> Generate(int initialValue, IList<int> intervals)
   {
      // work-around for Observable.Generate calling timeInterval before resultSelector
      intervals.Add(0);

      return Observable.Generate(initialValue,
         x => x < initialValue + intervals.Count - 1,
         x => x + 1,
         x => x,
         x => TimeSpan.FromSeconds(intervals[x - initialValue]));
   }
}