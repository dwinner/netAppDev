namespace VisitorTests;

[TestFixture]
public class Tests
{
   [Test]
   public void SimpleAddition()
   {
      var simple = new AdditionExpression(new Value(2), new Value(3));
      var ep = new ExpressionPrinter();
      ep.Visit(simple);
      Assert.That(ep.ToString(), Is.EqualTo("(2+3)"));
   }

   [Test]
   public void ProductOfAdditionAndValue()
   {
      var expr = new MultiplicationExpression(
         new AdditionExpression(new Value(2), new Value(3)),
         new Value(4)
      );
      var ep = new ExpressionPrinter();
      ep.Visit(expr);
      Assert.That(ep.ToString(), Is.EqualTo("(2+3)*4"));
   }
}