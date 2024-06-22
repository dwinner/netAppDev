namespace VisitorTests;

public abstract class ExpressionVisitor
{
   public abstract void Visit(Value value);

   public abstract void Visit(AdditionExpression ae);

   public abstract void Visit(MultiplicationExpression me);
}