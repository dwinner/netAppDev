namespace VisitorTests;

public abstract class Expression
{
   public abstract void Accept(ExpressionVisitor ev);
}