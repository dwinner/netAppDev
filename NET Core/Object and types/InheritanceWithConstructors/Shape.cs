using System;

internal class A
{
   internal void Foo()
   {
   }

   private class B
   {
   }
}

internal class B
{
   private void Bar()
   {
      var x = new A();
      x.Foo();
   }
}

public class Position
{
   public Position(int x, int y) => (X, Y) = (x, y);

   public int X { get; }
   public int Y { get; }

   public override string ToString() => $"X: {X}, Y: {Y}";
}

public class Size
{
   public Size(int width, int height) => (Width, Height) = (width, height);

   public int Width { get; }
   public int Height { get; }

   public override string ToString() => $"Width: {Width}, Height: {Height}";
}

public abstract class Shape
{
   protected Shape(int x, int y, int width, int height)
   {
      Position = new Position(x, y);
      Size = new Size(width, height);
   }

   public Position Position { get; }
   public virtual Size Size { get; }

   public void Draw() => DisplayShape();

   protected virtual void DisplayShape()
   {
      Console.WriteLine($"Shape with {Position} and {Size}");
   }

   public abstract Shape Clone();
}