namespace ClassicVisitor;

public interface IExpressionVisitor
{
   void Visit(DoubleExpression doubleExpr);

   void Visit(AdditionExpression additionExpr);
}