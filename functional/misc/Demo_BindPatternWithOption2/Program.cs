using LanguageExt;
using static System.Console;
using static FunctionalCsharp.Calculator;
using static FunctionalCsharp.IO;

var input = GetUserInput();
Validate(input);

//Func<string, Option<NonNegativeInteger>> verify =
//    x => ParseInput(x)
//    .BindWith(CheckNonNegativity); // OK too

//var result= verify(input);
//WriteLine(result);// Some(input) Or, None


namespace FunctionalCsharp
{
   public class NonNegativeInteger
   {
      public NonNegativeInteger(int number) =>
         // We do not allow any negative number
         Number = number >= 0 ? number : 0;

      public int Number { get; }

      public override string ToString() => Number.ToString();
   }

   public static class Calculator
   {
      /// <summary>
      ///    It validates the user's input
      /// </summary>
      public static Option<int> ParseInput(string input)
      {
         var flag = int.TryParse(input, out var initialNumber);
         return !flag
            ? Option<int>.None
            : initialNumber;
      }

      /// <summary>
      ///    It checks whether the integer is positive
      /// </summary>
      public static Option<NonNegativeInteger> CheckNonNegativity(int input) =>
         input < 0
            ? Option<NonNegativeInteger>.None
            : new NonNegativeInteger(input);

      //Bind:(C<T>,T->C<R>)->C<R>
      public static Option<NonNegativeInteger> BindWith(this Option<int> container,
         Func<int, Option<NonNegativeInteger>> f)
      {
         return container.Match
         (
            x => f(x),
            () => Option<NonNegativeInteger>.None
         );
      }

      // Generic Version
      public static Option<R> GenericBindWith<T, R>(this Option<T> container,
         Func<T, Option<R>> f)
      {
         return container.Match
         (
            x => f(x),
            () => Option<R>.None
         );
      }
   }

   public static class IO
   {
      public static string? GetUserInput()
      {
         WriteLine("Enter an integer:");
         var input = ReadLine();
         return input;
      }

      public static void Validate(string input)
      {
         // Using custom bind function: BindWith
         ParseInput(input)
            .BindWith(CheckNonNegativity)
            .Match(
               x => WriteLine($"Great.Entered a valid number: {x.Number}"),
               () => WriteLine("You did not enter a positive (or, valid) number.")
            );

         // Using custom bind function(generic version): GenericBindWith
         ParseInput(input)
            .GenericBindWith(CheckNonNegativity)
            .Match(
               x => WriteLine($"Great.Entered a valid number: {x}"),
               () => WriteLine("You did not enter a positive (or, valid) number.")
            );

         // Using inbuilt Bind from LanguageExt
         ParseInput(input)
            .Bind(CheckNonNegativity)
            .Match(
               x => WriteLine($"Great.Entered a valid number: {x}"),
               () => WriteLine("You did not enter a positive (or, valid) number.")
            );
      }
   }
}