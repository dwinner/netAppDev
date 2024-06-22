// disadvantages:

// 1) Performance penalty
// 2) Runtime error on missing visitor
// 3) Problematic w.r.t. inheritance

using System.Text;
using static System.Console;

namespace DynamicVisitor;

internal static class Program
{
   private static void Main()
   {
      var e = new AdditionExpression(
         new DoubleExpression(1),
         new AdditionExpression(
            new DoubleExpression(2),
            new DoubleExpression(3)));
      var ep = new ExpressionPrinter();
      var sb = new StringBuilder();
      ep.Print((dynamic)e, sb);
      WriteLine(sb);
   }
}