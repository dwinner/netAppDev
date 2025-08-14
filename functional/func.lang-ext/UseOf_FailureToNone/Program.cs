using System;
using LanguageExt;

namespace UseOf_FailureToNone;

// We can convert a 'failed' standard wrapped function to a None.
// This is helpful if you want to turn a failure into a 'valid'
// standard function either but with None Right value
internal static class Program
{
   private static void Main()
   {
      var result1 =
         from divideResult in DivideBy1(225, 66)
         from addResult in Add5ToIt1(divideResult).FailureToNone(_ => true)
         from calcResult in PerformPensionCalculations1(addResult).FailureToNone(_ => true)
         select calcResult;

      // this won't throw even though we failed, because we converted failures to None above!
      Console.WriteLine(
         $"The result is '{result1.ThrowIfFailed()}'");
   }

   private static Either<IAmFailure, Option<int>> PerformPensionCalculations1(Option<int> input)
   {
      var isGreaterThan100 =
         input.Match(number => number > 1,
            false); //match is like flattening an either in one common result type, in this case: bool
      return !isGreaterThan100
         ? (Either<IAmFailure, Option<int>>)new GenericFailure("Must be greater than 100")
         : input.Map(CalculateYourPension);
   }

   private static int PerformPensionCalculations(int input)
      => CalculateYourPension(input);

   private static int CalculateYourPension(int input)
      => input * 3 / 26;

   private static int DivideBy(int thisNumber, int dividedByThatNumber)
      => dividedByThatNumber == 0 ? 0 : thisNumber / dividedByThatNumber;

   private static Either<IAmFailure, Option<int>> DivideBy1(int thisNumber, int dividedByThatNumber) =>
      dividedByThatNumber == 0
         ? new GenericFailure("Can't divide by 0")
         : Option<int>.Some(thisNumber / dividedByThatNumber).ToSuccess();

   private static Either<IAmFailure, Option<int>> Add5ToIt1(Option<int> input)
   {
      var isValid = input.Match(number => number > 12, false);
      return !isValid
         ? (Either<IAmFailure, Option<int>>)new GenericFailure("must be greater than 12")
         : input.Map(validInput => validInput + 5);
   }

   private static int Add5ToIt(int input)
      => input + 5;
}