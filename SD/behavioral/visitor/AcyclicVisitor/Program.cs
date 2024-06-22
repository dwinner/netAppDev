using static System.Console;

namespace AcyclicVisitor;

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
   }
}