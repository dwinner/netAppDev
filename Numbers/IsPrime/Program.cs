/**
 * Простые числа
 */

using System;

namespace IsPrime
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         if (args.Length > 0 && int.TryParse(args[0], out var number))
            Console.WriteLine("Is {0} prime? ", IsPrime(number) ? "yes" : "no");
         else
            Console.WriteLine("Usage: IsPrime 12345");

         Console.ReadKey();
      }

      /// <summary>
      ///    Проверка, является ли число простым
      /// </summary>
      /// <param name="number">Число</param>
      /// <returns>true, если простое, false - в противном случае</returns>
      private static bool IsPrime(int number)
      {
         // Проверка на четность
         if (number % 2 == 0)
            return number == 2;

         // После извлечения квадратного корня проверка не нужна
         var max = (int) Math.Sqrt(number);
         for (var i = 3; i <= max; i += 2)
            if (number % i == 0)
               return false;
         return true;
      }
   }
}