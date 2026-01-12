using System;
using LanguageExt;

namespace UseOf_BiMap;

// BiMap()
internal static class Program
{
   private static void Main()
   {
      // Procedural way

      var step1 = DivideBy(225, 5);
      var step2 = Add5ToIt(step1);
      var result = PerformPensionCalculations(step2);

      // Fluent way
      var result1 = DivideBy1(225, 5)
         .Bind(input => Add5ToIt1(input))
         .Bind(input => PerformPensionCalculations1(input));

      // Transform both the invalid and valid versions of the options to a single string
      var results = result1.BiMap(_ => "Valid", () => "Invalid");

      Console.WriteLine($"The result is '{results}'");
   }

   private static Option<int> PerformPensionCalculations1(Option<int> input)
      => input.Map(CalculateYourPension);

   private static int PerformPensionCalculations(int input)
      => CalculateYourPension(input);

   private static int CalculateYourPension(int input)
      => input * 3 / 26;

   private static int DivideBy(int thisNumber, int dividedByThatNumber)
      => dividedByThatNumber == 0 ? 0 : thisNumber / dividedByThatNumber;

   private static Option<int> DivideBy1(int thisNumber, int dividedByThatNumber) =>
      dividedByThatNumber != 0
         ? thisNumber / dividedByThatNumber
         : Option<int>.None;

   private static Option<int> Add5ToIt1(Option<int> input)
      => input.Map(validInput => validInput + 5);

   private static int Add5ToIt(int input)
      => input + 5;
}