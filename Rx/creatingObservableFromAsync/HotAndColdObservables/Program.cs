using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Helpers;

namespace HotAndColdObservables
{
   internal static class Program
   {
      private static async Task Main()
      {
         await ColdObservableMultipleSubscriptionsExample()
            .ConfigureAwait(false);
         Console.ReadLine();
      }

      public static async Task ColdObservableMultipleSubscriptionsExample()
      {
         var coldObservable =
            Observable.Create<string>(async o =>
            {
               o.OnNext("Hello");
               await Task.Delay(TimeSpan.FromSeconds(1));
               o.OnNext("Rx");
            });

         coldObservable.SubscribeConsole("o1");
         await Task.Delay(TimeSpan.FromSeconds(0.5));
         coldObservable.SubscribeConsole("o2");
      }
   }
}