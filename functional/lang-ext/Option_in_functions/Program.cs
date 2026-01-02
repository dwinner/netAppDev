using System;
using LanguageExt;

namespace Option_in_functions;

// Contrived example of passing around Option<T> arguments
internal static class Program
{
   private static void Main()
   {
      var resultB = DivideBy1(225, 5);
      var result2 = Add5ToIt(resultB);

      // Now we can continue with the knowledge that result2 as 5 added to it or not (and everyone else will do that too:)
      var result = PerformPensionCalculations(result2);

      if (result.IsNone)
      {
         TheBadMessage();
      }

      if (result.IsSome)
      {
         TheGoodMessage();
      }

      // or we could use a BiIter without any conditionals above (if statements)
      result.BiIter(_ => TheGoodMessage(), TheBadMessage);
      return;

      void TheBadMessage() => Console.WriteLine("Could not determine your pension, because invalid input was used");

      void TheGoodMessage()
      {
         var pension = result.Match(i => i, 0);
         Console.WriteLine($"Your pension is '{pension}'");
      }
   }

   /// <summary>
   ///    Example of passing in an option monad
   /// </summary>
   private static Option<int> PerformPensionCalculations(Option<int> input) =>
      // Extract, Validate and transform it using Map (remember is a Monad)
      input.Map(CalculateYourPension);

   // we can call a normal function (non monad returning or accepting) inside a Map or Bind
   private static int CalculateYourPension(int input) => input * 3 / 26;

   private static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber) =>
      dividedByThatNumber == 0
         ? Option<int>.None
         : thisNumber / dividedByThatNumber;

   private static Option<int> Add5ToIt(Option<int> input) =>
      // View a Bind/Map as 'Try to add 5' if the input is valid ie not a None
      input.Bind(validInput =>
      {
         Option<int> option = validInput + 5;
         return option;
      });
}