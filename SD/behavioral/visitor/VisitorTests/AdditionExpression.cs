namespace VisitorTests;

public class AdditionExpression(Expression lhs, Expression rhs) : Expression
{
   public Expression Lhs { get; } = lhs;

   public Expression Rhs { get; } = rhs;

   public override void Accept(ExpressionVisitor ev)
   {
      ev.Visit(this);
   }
}