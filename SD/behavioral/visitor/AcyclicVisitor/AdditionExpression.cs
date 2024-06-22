namespace AcyclicVisitor;

public class AdditionExpression(ExpressionBase left, ExpressionBase right) : ExpressionBase
{
   public ExpressionBase Left { get; } = left;
   public ExpressionBase Right { get; } = right;

   public override void Accept(IVisitor aVisitor)
   {
      if (aVisitor is IVisitor<AdditionExpression> typed)
      {
         typed.Visit(this);
      }
   }
}