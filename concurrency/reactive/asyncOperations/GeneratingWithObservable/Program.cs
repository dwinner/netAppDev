using System.Reactive.Linq;
using Async.Shared;
using RxHelpers;

Demo.DisplayHeader("Generating primes into observable ");

var generator = new MagicalPrimeGenerator();

using var subscription = generator
   .GeneratePrimes_Sync(5)
   .Timestamp()
   .SubscribeConsole("primes observable");
Console.WriteLine("Generation is done");