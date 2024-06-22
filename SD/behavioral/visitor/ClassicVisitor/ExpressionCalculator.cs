namespace ClassicVisitor;

public class ExpressionCalculator : IExpressionVisitor
{
   public double Result { get; private set; }

   // what you really want is double Visit(...)

   public void Visit(DoubleExpression doubleExpr)
   {
      Result = doubleExpr.Value;
   }

   public void Visit(AdditionExpression additionExpr)
   {
      additionExpr.Left.Accept(this);
      var a = Result;
      additionExpr.Right.Accept(this);
      var b = Result;
      Result = a + b;
   }
}