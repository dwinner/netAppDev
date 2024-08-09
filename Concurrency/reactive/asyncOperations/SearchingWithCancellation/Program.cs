using System.Reactive.Disposables;
using System.Reactive.Linq;
using Async.Shared.SearchEngine;
using RxHelpers;

Demo.DisplayHeader("Creating async observable with async-await and cancellation");

var exampleResetEvent = new AutoResetEvent(false);

// Change the index to when you want the subscription disposed
var cancelIndex = 1;

var results = SearchEngineExample.Search_WithCancellation("Rx");
var subscription = Disposable.Empty;
subscription = results
   .Select((result, index) => new { result, index }) //adding the item index to the notification
   .Do(x =>
   {
      if (x.index == cancelIndex)
      {
         Console.WriteLine("Cancelling on index {0}", cancelIndex);
         subscription.Dispose();
         exampleResetEvent.Set();
      }
   })
   .Select(x => x.result) //rollback the observable to be IObservable<string> 
   .DoLast(() => exampleResetEvent.Set(), TimeSpan.FromSeconds(1))
   .SubscribeConsole("results");

exampleResetEvent.WaitOne();