/**
 * Конструкция Select и проекция
 */

using System;
using System.Linq;

namespace _06_SelectAndProjection
{
   class Program
   {
      static void Main()
      {
         int[] numbers = { 1, 2, 3, 4, 5 };
         var query = from x in numbers
                     select new { Input = x, Output = x * 2 } /*Result(x, x * 2)*/;
         foreach (var item in query)
         {
            Console.WriteLine("Input = {0}, Output = {1}",
               item.Input,
               item.Output);
         }

         Console.ReadKey();
      }
   }

   public class Result
   {
      public Result(int input, int output)
      {
         Input = input;
         Output = output;
      }

      public int Input { get; set; }
      public int Output { get; set; }
   }
}
