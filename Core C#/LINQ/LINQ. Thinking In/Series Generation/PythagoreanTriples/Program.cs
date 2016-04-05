// Генерирование правильных треугольников

using System;
using System.Linq;

namespace PythagoreanTriples
{
   internal static class Program
   {
      private static void Main()
      {
         var pythagoreanTriples =
            Enumerable.Range(2, 10).Select(c => new { Length = 2 * c, Height = c * c - 1, Hypotenuse = c * c + 1 });
         foreach (var triple in pythagoreanTriples)
         {
            Console.WriteLine(triple);
         }
      }
   }
}