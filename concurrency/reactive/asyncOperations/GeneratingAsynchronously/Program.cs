using Async.Shared;
using RxHelpers;

Demo.DisplayHeader("Using observable");

var generator = new MagicalPrimeGenerator();

var primesObservable = generator.GeneratePrimes_ManualAsync(5);
//primesObservable = generator.GeneratePrimes_AsyncCreate(5);
var subscription = primesObservable.SubscribeConsole("primes observable");

Console.WriteLine("Proving we're not blocked. you can continue typing. type X to dispose and exit");
while (true)
{
   var input = Console.ReadLine();
   if (input == "X")
   {
      subscription.Dispose();
      return;
   }

   Console.WriteLine("\t {0}", input);
}