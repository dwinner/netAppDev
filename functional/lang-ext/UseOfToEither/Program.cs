using LanguageExt;

namespace UseOfToEither;

// ToEither extension method to convert a value to a right sided Either<L,R>
internal static class Program
{
   private static void Main()
   {
      //Procedural way
      const int startingAmount = 225;
      var step1 = DivideBy(startingAmount, 5);
      var step2 = Add5ToIt(step1);
      var result = PerformPensionCalculations(step2);

      // Fluent way
      var result1 = DivideBy1(startingAmount, 5)
         .Bind(input => Add5ToIt1(input))
         .Bind(input => PerformPensionCalculations1(input));

      // Some value of the Option<T> ie the integer will be the right value of either,
      // and then you need to choose a left value:
      var either = result1.ToEither(() => 23);
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