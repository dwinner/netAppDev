namespace ReflectiveVisitor;

public class AdditionExpression(Expression left, Expression right) : Expression
{
   public Expression Left { get; } = left ?? throw new ArgumentNullException(nameof(left));

   public Expression Right { get; } = right ?? throw new ArgumentNullException(nameof(right));
}