/**
 * Ковариантность и контравариантность
 */

using System;

namespace Variance
{
   static class Program
   {
      static void Main()
      {
         // Ковариантность
         IIndex<Rectangle> rectangles = RectangeCollection.Rectangles;
         IIndex<Shape> shapes = rectangles;
         for (int i = 0; i < shapes.Count; i++)
         {
            Console.WriteLine(shapes[i]);
         }

         // Контравариантность
         IDisplay<Shape> shapeDisplay = new ShapeDisplay();
         IDisplay<Rectangle> rectangeDisplay = shapeDisplay;
         rectangeDisplay.Show(rectangles[0]);

         Console.ReadKey();
      }
   }
}
