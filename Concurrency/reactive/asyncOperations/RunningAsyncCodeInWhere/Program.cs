using System.Reactive.Linq;
using Async.Shared.SearchEngine;
using Async.Shared.Services;
using RxHelpers;

Demo.DisplayHeader("Deferred async");
var results = SearchEngineExample.Search_DefferedConcatingTasks("Rx");
results.RunExample("deferred");

Demo.DisplayHeader("Running async code int the pipeline - order is not determenistic");

var svc = new PrimeCheckService();
var primes =
   from number in Observable.Range(1, 10)
   from isPrime in svc.IsPrimeAsync(number)
   where isPrime
   select number;
primes.SubscribeConsole();
//
// The same, but in methods chain
//
primes =
   Observable.Range(1, 10)
      .SelectMany(number => svc.IsPrimeAsync(number),
         (number, isPrime) => new { number, isPrime })
      .Where(x => x.isPrime)
      .Select(x => x.number);

var exampleResetEvent = new AutoResetEvent(false);
primes
   .DoLast(() => exampleResetEvent.Set(), TimeSpan.FromSeconds(1))
   .SubscribeConsole("primes");

exampleResetEvent.WaitOne();