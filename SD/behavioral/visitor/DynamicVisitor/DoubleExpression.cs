namespace DynamicVisitor;

public class DoubleExpression(double value) : Expression
{
   public double Value { get; } = value;
}