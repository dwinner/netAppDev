using LanguageExt;
using static System.Console;

var dividend = new Random().Next(10, 12);
var divisor = new Random().Next(3);
WriteLine($"Dividend: {dividend}, Divisor: {divisor}");
var value4 = GetQuotient(dividend, divisor)
   .Match
   (
      x => WriteLine($"{dividend}/{divisor}= {x}"),
      () => WriteLine("Error: Divisor becomes Zero.")
   );

static Option<int> GetQuotient(int a, int b) =>
   b != 0
      // ? Option<int>.Some(a / b)
      ? a / b
      : Option<int>.None;