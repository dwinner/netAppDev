/*
 * Методы расширения.
 */
using System;

namespace ExtensionMethods
{
   // Методы расширения должны быть определены в статическом классе
   static class IntMethods
   {
      // Методы расширения должны быть статическими
      // Ключевое слово this говорит C#, что это метод расширения
      public static bool IsPrime(this int number)
      {
         if (number % 2 == 0) // Проверка четности
         {
            return number == 2;
         }
         var max = (int)Math.Sqrt(number);
         for (int i = 3; i <= max; i += 2)
         {
            if ((number % i) == 0)
            {
               return false;
            }
         }
         return true;
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         for (int i = 0; i < 100; ++i)
         {
            if (i.IsPrime())
            {
               Console.WriteLine(i);
            }
         }
         Console.ReadKey();
      }
   }
}
