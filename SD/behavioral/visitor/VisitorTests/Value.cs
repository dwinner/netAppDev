namespace VisitorTests;

public class Value(int value) : Expression
{
   public readonly int TheValue = value;

   public override void Accept(ExpressionVisitor ev)
   {
      ev.Visit(this);
   }
}