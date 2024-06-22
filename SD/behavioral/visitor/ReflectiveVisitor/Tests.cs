using System.Text;
using static System.Console;

namespace ReflectiveVisitor;

public class Tests
{
   [Test]
   public void ReflVisitorTest()
   {
      var e = new AdditionExpression(
         new DoubleExpression(1),
         new AdditionExpression(
            new DoubleExpression(2),
            new DoubleExpression(3)));
      var sb = new StringBuilder();
      e.Print2(sb); // extension method goodness!
      WriteLine(sb);
   }
}