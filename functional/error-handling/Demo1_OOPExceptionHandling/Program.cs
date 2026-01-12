using static System.Console;

WriteLine("***Case study on exception handling in OOP.***");
var dividend = new Random().Next(10, 12);
var divisor = new Random().Next(3);
WriteLine($"Dividend: {dividend}, Divisor: {divisor}");
var quotient = 0;

try
{
   quotient = Calculator.GetQuotient(dividend, divisor);
}
catch (Exception e)
{
   WriteLine($"Error:{e}");
}

WriteLine($"Quotient: {quotient}");

internal class Calculator
{
   public static int GetQuotient(int a, int b) => a / b;
}