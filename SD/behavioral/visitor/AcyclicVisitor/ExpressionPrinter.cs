using System.Text;

namespace AcyclicVisitor;

public class ExpressionPrinter : IVisitor,
   IVisitor<ExpressionBase>,
   IVisitor<DoubleExpression>,
   IVisitor<AdditionExpression>
{
   private readonly StringBuilder _stringBuilder = new();

   public void Visit(AdditionExpression aVisitable)
   {
      _stringBuilder.Append('(');
      aVisitable.Left.Accept(this);
      _stringBuilder.Append('+');
      aVisitable.Right.Accept(this);
      _stringBuilder.Append(')');
   }

   public void Visit(DoubleExpression aVisitable)
   {
      _stringBuilder.Append(aVisitable.Value);
   }

   public void Visit(ExpressionBase aVisitable)
   {
      // default handler?
   }

   public override string ToString() => _stringBuilder.ToString();
}