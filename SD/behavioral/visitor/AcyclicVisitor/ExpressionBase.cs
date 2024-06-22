namespace AcyclicVisitor;

public abstract class ExpressionBase
{
   public virtual void Accept(IVisitor aVisitor)
   {
      if (aVisitor is IVisitor<ExpressionBase> typed)
      {
         typed.Visit(this);
      }
   }
}