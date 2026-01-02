using LanguageExt;

namespace Pipelining_with_Options;

// Rosetta code! Procedural -> Fluent -> Query Syntax
internal static class Program
{
   private static void Main()
   {
      const int startingAmount = 225;

      // Procedural way
      var step1 = DivideBy(startingAmount, 5);
      var step2 = Add5ToIt(step1);
      var result = PerformPensionCalculations(step2);

      // Fluent way
      var result1 = DivideBy1(startingAmount, 5)
         .Bind(input => Add5ToIt1(input))
         .Bind(input => PerformPensionCalculations1(input));

      // Expression way 
      var result2 =
         from input1 in DivideBy1(startingAmount, 5)
         from input2 in Add5ToIt1(input1)
         from input3 in PerformPensionCalculations1(input2)
         select input3;

      // Oh Wow, we've really simplified the code, it's smaller, all the steps are
      // functions, and we are happy passing and sending Monads have helped us create
      // step-by-step logical means to execute code entirely from functions!
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