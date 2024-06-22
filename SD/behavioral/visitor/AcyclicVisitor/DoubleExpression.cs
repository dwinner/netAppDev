namespace AcyclicVisitor;

public class DoubleExpression(double value) : ExpressionBase
{
   public double Value { get; } = value;

   public override void Accept(IVisitor aVisitor)
   {
      if (aVisitor is IVisitor<DoubleExpression> typed)
      {
         typed.Visit(this);
      }
   }
}