using static System.Console;

namespace ClassicVisitor;

internal static class Program
{
   private static void Main()
   {
      var expr = new AdditionExpression(
         new DoubleExpression(1),
         new AdditionExpression(
            new DoubleExpression(2),
            new DoubleExpression(3)));
      var ep = new ExpressionPrinter();
      ep.Visit(expr);
      WriteLine(ep.ToString());

      var calc = new ExpressionCalculator();
      calc.Visit(expr);
      WriteLine($"{ep} = {calc.Result}");
   }
}