using System;

public abstract class Shape
{
   public Position Position { get; } = new();
   public virtual Size Size { get; } = new();

   public void Draw() => DisplayShape();

   protected virtual void DisplayShape()
   {
      Console.WriteLine($"Shape with {Position} and {Size}");
   }

   public virtual void Move(Position newPosition)
   {
      Position.X = newPosition.X;
      Position.Y = newPosition.Y;
      Console.WriteLine($"moves to {Position}");
   }

   public abstract Shape Clone();
}