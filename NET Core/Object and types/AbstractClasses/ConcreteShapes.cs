using System;

public class Rectangle : Shape
{
   protected override void DisplayShape()
   {
      Console.WriteLine($"Rectangle at position {Position} with size {Size}");
   }

   public override void Move(Position newPosition)
   {
      Console.Write("Rectangle ");
      base.Move(newPosition);
   }

   public override Rectangle Clone()
   {
      Rectangle r = new()
      {
         Position =
         {
            X = Position.X,
            Y = Position.Y
         },
         Size =
         {
            Width = Size.Width,
            Height = Size.Width
         }
      };

      return r;
   }
}

public class Ellipse : Shape
{
   protected override void DisplayShape()
   {
      Console.WriteLine($"Ellipse at position {Position} with size {Size}");
   }

   public override void Move(Position newPosition)
   {
      Console.Write("Ellipse ");
      base.Move(newPosition);
   }

   public override Ellipse Clone()
   {
      Ellipse e = new()
      {
         Position =
         {
            X = Position.X,
            Y = Position.Y
         },
         Size =
         {
            Width = Size.Width,
            Height = Size.Width
         }
      };

      return e;
   }
}