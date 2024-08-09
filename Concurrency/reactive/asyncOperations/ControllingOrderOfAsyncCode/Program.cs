using System.Reactive.Linq;
using Async.Shared.Services;
using RxHelpers;

Demo.DisplayHeader("Controlling the order of async code with Concat");

var resetEvent = new AutoResetEvent(false);

Console.WriteLine("Using SelectMany wont maintain items order");
var svc = new VariableTimePrimeCheckService(3);
var primes =
   from number in Observable.Range(1, 10)
   from isPrime in svc.IsPrimeAsync(number)
   where isPrime
   select number;

primes
   .DoLast(() => resetEvent.Set(), TimeSpan.FromSeconds(1))
   .SubscribeConsole("primes - unordered");

// Waiting for the previous example to finish
resetEvent.WaitOne();

Console.WriteLine("Using concat does enforce order");
primes =
   Observable.Range(1, 10)
      .Select(async number => new
      {
         number,
         IsPrime = await svc.IsPrimeAsync(number).ConfigureAwait(false)
      })
      .Concat()
      .Where(x => x.IsPrime)
      .Select(x => x.number);

primes.SubscribeConsole("primes");

// Waiting for the previous example to finish
resetEvent.WaitOne();