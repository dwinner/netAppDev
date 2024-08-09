using Async.Shared;
using RxHelpers;

Demo.DisplayHeader("Using synchronous enumerable");

var generator = new MagicalPrimeGenerator();
// this will block the main thread for a few seconds
foreach (var prime in generator.Generate(5))
{
   Console.Write("{0}, ", prime);
}