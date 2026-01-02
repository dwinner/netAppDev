using LanguageExt;
using static System.Console;

WriteLine("***Demonstration 2: exception handling in FP.***");

var dividend = new Random().Next(10, 12);
var divisor = new Random().Next(3);
WriteLine($"Dividend: {dividend}, Divisor: {divisor}");
var result = Calculator.GetQuotient(dividend, divisor);

//// If you want to process the valid scenario only
//result.IfRight(x => WriteLine($"(Using IfRight),Quotient: {result.Case}"));
//// OR
//if (result.IsRight) { WriteLine($"(Using IsRight),Quotient: {result.Case}"); }

// If we want to handle both scenarios
result.Match(
   success => WriteLine($"Quotient={success}"),
   error => WriteLine($"Error: {error}")
);

//namespace FunctionalCsharp
//{
internal class Calculator
{
   public static Either<Exception, int> GetQuotient(int a, int b) =>
      b == 0
         ? new DivideByZeroException("Divisor becomes Zero.")
         : a / b;
}
//}