using System.Reactive.Linq;
using RxHelpers;

namespace RxMagicalPrimeGeneratorExample;

internal static class Program
{
   private static void Main()
   {
      //GeneratingSynchronously();
      //GeneratingEnumerableAsynchronously().Wait();
      //GeneratingWithObservable();
      GeneratingAsynchronously();
      Console.WriteLine("Done");
      Console.ReadLine();
   }

   private static void GeneratingWithObservable()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Generating primes into observable ");

      var generator = new MagicalPrimeGenerator();

      var subscription = generator
         .GeneratePrimes_Sync(5)
         .Timestamp()
         .SubscribeConsole("primes observable");

      Console.WriteLine("Generation is done");
   }

   private static async Task GeneratingEnumerableAsync()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Generating enumerable asynchronously");

      var generator = new MagicalPrimeGenerator();

      Console.WriteLine("it will takes a 10 seconds before anything will be printed");
      foreach (var prime in await generator.GenerateAsync(5).ConfigureAwait(false))
      {
         Console.Write("{0}, ", prime);
      }
   }

   private static void GeneratingSynchronously()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Using synchronous enumerable");

      var generator = new MagicalPrimeGenerator();
      // this will block the main thread for a few seconds
      foreach (var prime in generator.Generate(5))
      {
         Console.Write("{0}, ", prime);
      }
   }

   private static void GeneratingAsynchronously()
   {
      Console.WriteLine();
      Demo.DisplayHeader("Using observable");

      var generator = new MagicalPrimeGenerator();

      var primesObservable = generator.GeneratePrimes_ManualAsync(5);
      //primesObservable = generator.GeneratePrimes_AsyncCreate(5);
      var subscription =
         primesObservable
            .SubscribeConsole("primes observable");

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
   }
}