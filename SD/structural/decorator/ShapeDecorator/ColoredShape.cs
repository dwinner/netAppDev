namespace ShapeDecorator;

public class ColoredShape(Shape shape, string color) : Shape
{
   public override string AsString() => $"{shape.AsString()} has the color {color}";
}

public class ColoredShape<T>(string color) : Shape
   where T : Shape, new()
{
   private readonly T _shape = new();

   public ColoredShape() : this("black")
   {
   }

   // no constructor forwarding

   public override string AsString() => $"{_shape.AsString()} has the color {color}";
}