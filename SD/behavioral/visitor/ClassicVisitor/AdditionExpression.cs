namespace ClassicVisitor;

public class AdditionExpression(ExpressionBase left, ExpressionBase right) : ExpressionBase
{
   public ExpressionBase Left { get; } = left;

   public ExpressionBase Right { get; } = right;

   public override void Accept(IExpressionVisitor visitor)
   {
      visitor.Visit(this);
   }
}