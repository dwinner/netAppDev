/**
 * Специальное форматирование объектов
 */

using System;

namespace FormattableVector
{
   static class Program
   {
      static void Main()
      {
         var firstVector = new Vector(1, 32, 5);
         var secondVector = new Vector(845.4, 54.3, -7.8);
         Console.WriteLine("\nIn IJK format,\nv1 is {0,30:IJK}\nv2 is {1,30:IJK}", firstVector, secondVector);
         Console.WriteLine("\nIn default format,\nv1 is {0,30}\nv2 is {1,30}", firstVector, secondVector);
         Console.WriteLine("\nIn VE format\nv1 is {0,30:VE}\nv2 is {1,30:VE}", firstVector, secondVector);
         Console.WriteLine("\nNorms are:\nv1 is {0,20:N}\nv2 is {1,20:N}", firstVector, secondVector);
      }
   }
}
