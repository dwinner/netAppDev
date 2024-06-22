namespace ClassicVisitor;

public class DoubleExpression(double value) : ExpressionBase
{
   public double Value { get; } = value;

   public override void Accept(IExpressionVisitor visitor)
   {
      visitor.Visit(this);
   }
}