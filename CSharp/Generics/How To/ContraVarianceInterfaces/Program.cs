using System;
using System.Collections.Generic;

namespace ContraVarianceInterfaces
{
   class Shape
   {
      public int Id;
   }

   class Rectangle : Shape
   {

   }

   class ShapeComparer : IComparer<Shape>
   {
      public int Compare(Shape x, Shape y)
      {
         return x.Id.CompareTo(y.Id);
      }
   }

   class Program
   {
      static void Main()
      {
         ShapeComparer shapeComparer = new ShapeComparer();

         // Это вполне законно, т.к. IComparer контравариантен
         IComparer<Shape> ic = shapeComparer;         

         IComparer<Rectangle> irc = shapeComparer;         

         Console.ReadKey();
      }
   }
}
