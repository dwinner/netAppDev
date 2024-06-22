namespace ReflectiveVisitor;

public class DoubleExpression(double value) : Expression
{
   public double Value { get; } = value;
}