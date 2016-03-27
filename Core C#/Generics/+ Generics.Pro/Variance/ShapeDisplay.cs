using System;

namespace Variance
{
   public class ShapeDisplay : IDisplay<Shape>
   {
      public void Show(Shape shape)
      {
         Console.WriteLine("{0} Width: {1}, Height: {2}", shape.GetType().Name, shape.Width, shape.Height);
      }
   }
}