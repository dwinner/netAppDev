using System;
using LanguageExt;

const string word = "five";
Either<int, string> amount = word;

// like BiBind() allows you to provide both transform functions and the correct transform will run
// depending on is it is a right or left type contained within it - you can do something similar here.
// BiExists allows you to test/use/inspect the content and return true/false based on it.
// BiExists can be viewed as a the 'existence' of a provided validation check being successful,
// specific to each type left or right
var isEitherGreaterThanNothing = amount.BiExists(
   stringRight => stringRight.Length > 0,
   integerLeft => integerLeft > 0
);

Console.WriteLine($"The result value is {isEitherGreaterThanNothing}");