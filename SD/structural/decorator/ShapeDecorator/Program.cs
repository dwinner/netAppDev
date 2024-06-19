using static System.Console;

namespace ShapeDecorator;

internal static class Program
{
   private static void Main()
   {
      var circle = new Circle(2);
      WriteLine(circle.AsString());

      var redSquare = new ColoredShape(circle, "red");
      WriteLine(redSquare.AsString());

      var redHalfTransparentSquare = new TransparentShape(redSquare, 0.5f);
      WriteLine(redHalfTransparentSquare.AsString());

      // static
      var blueCircle = new ColoredShape<Circle>("blue");
      WriteLine(blueCircle.AsString());
      // A circle of radius 0 has the color blue

      var blackHalfSquare = new TransparentShape<ColoredShape<Square>>(0.4f);
      WriteLine(blackHalfSquare.AsString());
      // A square with side 0 has the color black has transparency 40
   }
}