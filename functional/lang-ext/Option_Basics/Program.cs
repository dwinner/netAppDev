using System;
using LanguageExt;

namespace Option_Basics;

internal static class Program
{
   private static void Main()
   {
      var resultA = DivideBy(25, 5);
      var resultB = DivideBy1(25, 0);

      Console.WriteLine($"The result A is '{resultA}' and B is '{resultB}'");

      /*
       * Discussion:
       * DivideBy1 will return an Option<T> and it knows what an invalid result is - its encapsulated within the option<T>
       * DivideBy will return an int, but the caller needs to know that 0 is an invalid result.
       */

      var result1 = Add5ToIt(resultA);
      var result2 = Add5ToIt(resultB);

      Console.WriteLine($"The result A is '{result1}' and B is '{result2}'");
   }


   /// <summary>
   ///    Normal function, not using optional parameters
   /// </summary>
   /// <param name="thisNumber"></param>
   /// <param name="dividedByThatNumber"></param>
   /// <returns>integer</returns>
   private static int DivideBy(int thisNumber, int dividedByThatNumber)
   {
      if (dividedByThatNumber == 0)
      {
         return 0;
      }

      return thisNumber / dividedByThatNumber;
   }

   /// <summary>
   ///    Function returns Monad, one construct that represents both failure and success.
   /// </summary>
   /// <returns>Option of an integer</returns>
   private static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber)
   {
      if (dividedByThatNumber == 0)
      {
         var t = Option<int>.None;
         return t;
      }

      return thisNumber / dividedByThatNumber;
   }

   private static int Add5ToIt(int input) =>
      // Whoops, I've forgotten to check if input is valid.
      input + 5;

   // And even if I did, I would have to know what an invalid input is - its 0 in this case.
   private static Option<int> Add5ToIt(Option<int> input)
   {
      // I can assume that its valid, because Map will run a transformation function on the valid input
      var t = input.Map(validInput => validInput + 5);

      // if its invalid the Validation phase of the map() function will return a None ie an invalid input and so
      // the result of the Map will be an option<T> of None or invalid input in it.
      // Remember a Map always returns a Monad, in this case Option<T> and it automatically lifts it for you by Map()
      return t;
   }
}