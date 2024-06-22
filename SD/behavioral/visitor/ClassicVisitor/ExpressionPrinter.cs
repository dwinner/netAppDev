using System.Text;

namespace ClassicVisitor;

public class ExpressionPrinter : IExpressionVisitor
{
   private readonly StringBuilder _stringBuilder = new();

   public void Visit(DoubleExpression doubleExpr)
   {
      _stringBuilder.Append(doubleExpr.Value);
   }

   public void Visit(AdditionExpression additionExpr)
   {
      _stringBuilder.Append('(');
      additionExpr.Left.Accept(this);
      _stringBuilder.Append('+');
      additionExpr.Right.Accept(this);
      _stringBuilder.Append(')');
   }

   public override string ToString() => _stringBuilder.ToString();
}