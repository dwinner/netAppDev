using System.Reactive.Linq;

namespace RunAsyncOnDemand;

internal static class Program
{
   private static void Main()
   {
      for (var i = 0; i < 10; i++)
      {
         LongRunningOperationAsync(i.ToString()).Subscribe(Console.WriteLine);
      }

      Console.ReadKey();
   }

   // Synchronous operation
   private static string DoLongRunningOperation(string param)
   {
      Console.WriteLine(param);
      return $"#{param}#";
   }

   private static IObservable<string> LongRunningOperationAsync(string param) =>
      Observable.Create<string>(
         observer => Observable
            .ToAsync<string, string>(DoLongRunningOperation)(param)
            .Subscribe(observer)
      );
}