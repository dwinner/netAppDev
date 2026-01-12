using System;
using LanguageExt;

// Demonstrates the usage of IfNone and IfSome which runs a user defined function provided the option is None or Some respectively
namespace IfSome_and_IfNone;

internal static class Program
{
   private static void Main()
   {
      var resultB = DivideBy1(225, 5);
      var result2 = Add5ToIt(resultB);

      // Now we can continue with the knowledge that result2 as 5 added to it or not (and everyone else can do that too...)
      var result = PerformPensionCalculations(result2);

      var noValue = result.IfSome(validInput => Console.WriteLine($"yay valid input is {validInput}"));
      var defaultPensionIfInvalidInput = result.IfNone(() => -1);
      // IfNone turns your result into a valid value, effectively a Some() but its actual value, int
   }

   private static Option<int> PerformPensionCalculations(Option<int> input)
   {
      // Extract, Validate and transform it using Map
      return input.Map(CalculateYourPension);
      // We can call a normal function (non monad returning or accepting) inside a Map or Bind
   }

   private static int CalculateYourPension(int input) => input * 3 / 26;

   private static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber)
   {
      if (dividedByThatNumber == 0)
      {
         var noneOpt = Option<int>.None;
         return noneOpt;
      }

      return thisNumber / dividedByThatNumber;
   }

   private static Option<int> Add5ToIt(Option<int> input)
   {
      // View a Bind/Map as 'Try to add 5' if the input is valid ie not a None
      return input.Bind(validInput =>
      {
         Option<int> option = validInput + 5;
         return option;
      });
   }
}