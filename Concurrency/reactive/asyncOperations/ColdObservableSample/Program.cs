using System.Reactive.Linq;
using RxHelpers;

await ColdObservableMultipleSubscriptionsExampleAsync()
   .ConfigureAwait(false);
Console.ReadLine();
return;

static async Task ColdObservableMultipleSubscriptionsExampleAsync()
{
   var coldObservable =
      Observable.Create<string>(async o =>
      {
         o.OnNext("Hello");
         await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
         o.OnNext("Rx");
      });

   coldObservable.SubscribeConsole("o1");
   await Task.Delay(TimeSpan.FromSeconds(0.5)).ConfigureAwait(false);
   coldObservable.SubscribeConsole("o2");
}