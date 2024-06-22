namespace ClassicVisitor;

public abstract class ExpressionBase
{
   public abstract void Accept(IExpressionVisitor visitor);
}