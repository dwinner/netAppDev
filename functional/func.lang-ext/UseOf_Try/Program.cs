using System;
using LanguageExt;

namespace UseOf_Try;

// This tutorial demonstrates the use of the Try<> Monad.
internal static class Program
{
   private static void Main()
   {
      // What we can do now is wrap this unsafe function, into a Try<> which will capture any
      // exceptions that are thrown (they don't leave the function)
      var try1 = new Try<int>(() =>
      {
         var result = SomeExternalCode(25, 5);
         return result;
      });

      var try2 = new Try<int>(() =>
      {
         var result = SomeExternalCode(25, 0);
         return result;
      });

      // Now we have encapsulated any exceptions into the Try<> type, we can now check if it had any failures otherwise use the result    
      var result1 = try1.Match(
         unit => unit.ToEither<IAmFailure, int>(), // was ok, no exceptions thrown
         exception =>
            new ExternalLibraryFailure(
               exception)); // We got an exception, now how should we deal with it - lets turn it into a IAmFailure type

      var result2 = try2.Match(
         unit => unit.ToEither<IAmFailure, int>(), // was ok, no exceptions thrown
         exception =>
            new ExternalLibraryFailure(
               exception)); // We got an exception, now how should we deal with it - lets turn it into a IAmFailure type

      // Print the results
      Console.WriteLine($"Result1 was: {result1} and Result2 was {result2}");

      // And if you required all/both to succeed you could use short-circuiting behavior in a pipeline
      var overallResult =
         from res1 in result1
         from res2 in result2
         select res1 / res2;

      Console.WriteLine($"The combined result is {overallResult}");
      return;

      // This is a function that could throw an exception.
      // We don't want that because that would jump straight out of our function
      // and cause our function not to complete fully.
      // We want robust, dependable functions that always return no matter what.
      int SomeExternalCode(int numerator, int denominator)
      {
         if (denominator == 0)
         {
            throw new DivideByZeroException();
         }

         return numerator / denominator;
      }
   }
}