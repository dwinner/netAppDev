namespace ShapeDecorator;

public sealed class Circle(float radius) : Shape
{
   public Circle() : this(0)
   {
   }

   public void Resize(float factor)
   {
      radius *= factor;
   }

   public override string AsString() => $"A circle of radius {radius}";
}