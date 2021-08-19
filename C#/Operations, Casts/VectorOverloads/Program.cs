/**
 * Перегрузка операторов
 */

using System;

namespace VectorOverloads
{
   static class Program
   {
      static void Main()
      {
         var vector1 = new Vector(3.0, 3.0, 1.0);
         var vector2 = new Vector(2.0, -4.0, -4.0);
         Vector vector3 = vector1 + vector2;

         Console.WriteLine("Vector1 = {0}", vector1);
         Console.WriteLine("Vector2 = {0}", vector2);
         Console.WriteLine("Vector3 = " + vector3);

         Console.WriteLine("2 * Vector3 = {0}", 2 * vector3);

         vector3 += vector2;
         Console.WriteLine("Vector3 += Vector2: {0}", vector3);

         vector3 = vector1 * 2;
         Console.WriteLine("Vector3 = Vector1 * 2: {0}", vector3);

         Vector multVector = vector1 * vector2;
         Console.WriteLine("vector1 * vector2: {0}", multVector);

         Console.WriteLine("Scalar: {0}", VectorExtension.ScalarMultiply(vector1, vector2));

         Console.ReadLine();
      }
   }
}
