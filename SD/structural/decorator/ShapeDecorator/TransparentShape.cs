namespace ShapeDecorator;

public class TransparentShape(Shape shape, float transparency) : Shape
{
   public override string AsString() =>
      $"{shape.AsString()} has {transparency * 100.0f}% transparency";
}

public class TransparentShape<T>(float transparency) : Shape
   where T : Shape, new()
{
   private readonly T _shape = new();

   public override string AsString() => $"{_shape.AsString()} has transparency {transparency * 100.0f}";
}