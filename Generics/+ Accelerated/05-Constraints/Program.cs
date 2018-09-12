/**
 * Ограничения
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_Constraints
{
   class Program
   {
      static void Main()
      {
         var shapes = new Shapes<IShape>();
         shapes.Add(new Circle(2));
         shapes.Add(new Rect(3, 5));
         Console.WriteLine("Общая площадь: {0}", shapes.TotalArea);

         Console.ReadKey();
      }
   }

   public interface IShape
   {
      double Area { get; }
   }

   public class Circle : IShape
   {
      private readonly double _radius;

      public Circle(double radius) { _radius = radius; }

      public double Area { get { return Math.PI * _radius * _radius; } }
   }

   public class Rect : IShape
   {
      private readonly double _width;
      private readonly double _height;

      public Rect(double width, double height)
      {
         _width = width;
         _height = height;
      }

      public double Area { get { return _width * _height; } }
   }

   public class Shapes<T>
      where T : IShape
   {
      private readonly IList<T> _shapes = new List<T>();

      public double TotalArea
      {
         get { return _shapes.Aggregate(0.0, (accumulator, shape) => accumulator + shape.Area); }
      }

      public void Add(T aShape) { _shapes.Add(aShape); }
   }
}
