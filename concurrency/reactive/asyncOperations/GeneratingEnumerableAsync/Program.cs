using Async.Shared;
using RxHelpers;

Demo.DisplayHeader("Generating enumerable asynchronously");

var generator = new MagicalPrimeGenerator();

Console.WriteLine("it will takes a 10 seconds before anything will be printed");
foreach (var prime in await generator.GenerateAsync(5).ConfigureAwait(false))
{
   Console.Write("{0}, ", prime);
}