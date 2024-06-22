using System.Text;

namespace VisitorTests;

public class ExpressionPrinter : ExpressionVisitor
{
   private readonly StringBuilder _stringBuilder = new();

   public override void Visit(Value value)
   {
      _stringBuilder.Append(value.TheValue);
   }

   public override void Visit(AdditionExpression ae)
   {
      _stringBuilder.Append('(');
      ae.Lhs.Accept(this);
      _stringBuilder.Append('+');
      ae.Rhs.Accept(this);
      _stringBuilder.Append(')');
   }

   public override void Visit(MultiplicationExpression me)
   {
      me.Lhs.Accept(this);
      _stringBuilder.Append('*');
      me.Rhs.Accept(this);
   }

   public override string ToString() => _stringBuilder.ToString();
}