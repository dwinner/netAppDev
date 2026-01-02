using static System.Console;

//WriteLine("Exercise 3.4");
Circle circle = new(7.5, "Red");
WriteLine($"Current detail: {circle}");
var updatedCircle = circle.IncrementRadiusBy(2);
WriteLine($"Current detail: {updatedCircle}");

internal class Circle
{
   public Circle(double radius, string color)
   {
      Radius = radius;
      Color = color;
   }

   public double Radius { get; set; }

   public string Color { get; set; }

   public Circle IncrementRadiusBy(int r) => new(Radius + r, Color);

   public override string ToString() =>
      $"Radius:{Radius} units, Color: {Color}";
}