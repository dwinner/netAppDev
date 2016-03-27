/**
 * Контравариантность делегатов
 */

using System;

namespace ContraVarianceDemo
{
   class Shape
   {
      public void Draw() { Console.WriteLine("Drawing shape"); }
   };

   class Rectangle : Shape
   {
      public void Expand() { /*...*/ }
   };

   class Program
   {
      delegate void ShapeAction<in T>(T shape);

      static void DrawShape(Shape shape)
      {
         if (shape != null)
         {
            shape.Draw();
         }
      }

      static void Main()
      {
         // Очевидно, что этот код должен работать
         ShapeAction<Shape> action = DrawShape;
         action(new Rectangle());

         /*
          * Интуитивно понятно, что любой метод, удовлетворяющий делегату
          * ShapeAction<Shape>, должен работать с объектом Rectangle, потому что
          * Rectangle является производным от Shape
          * 
          * Всегда есть возможность присвоить менее специфичный метод
          * более специфичному делегату, но до появления версии .NET 4
          * нельзя было присвоить менее специфичный делегат более специфичному
          * делегату. Это очень важное различие.
          * 
          * Теперь это можно, поскольку параметр-тип помечен модификатором "in".
          */

         // Следующие действия были возможны до появления .NET 4
         ShapeAction<Rectangle> rectAction1 = DrawShape;
         rectAction1(new Rectangle());

         // А это было невозможно до появления .NET 4
         ShapeAction<Rectangle> rectAction2 = action;
         rectAction2(new Rectangle());

         Console.ReadKey();
      }
   }
}

