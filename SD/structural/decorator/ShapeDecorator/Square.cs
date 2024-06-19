namespace ShapeDecorator;

public sealed class Square(float side) : Shape
{
   public Square() : this(0)
   {
   }

   public override string AsString() => $"A square with side {side}";
}