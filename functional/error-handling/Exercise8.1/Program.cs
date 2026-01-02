using LanguageExt;
using static System.Console;


// Exercise 8.1

var input = 250;
GetResult(input).Match(
   Left: e => WriteLine($"{e.Message}"),
   Right: WriteLine
//Same as:
//Right: x => WriteLine(x)
);

input = 7250;
GetResult(input).Match(
   Left: e => WriteLine($"{e.Message}"),
   Right: WriteLine
//Same as:
//Right: x => WriteLine(x)
);

static Either<Exception, string> GetResult(int a) =>
   a > 5000
      ? new Exception("Please try a number less than 5000.")
      : $"You have entered {a}.";