/*
 * Захваченные переменные и замыкания
 */

using System;

namespace _07_Closures
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         // Создать массив целых чисел
         int[] integers = { 1, 2, 3, 4 };

         var factor = new Factor(2);
         var proc = new Processor { Strategy = factor.Multiplier };
         PrintArray(proc.Process(integers));

         proc.Strategy = factor.Adder;
         PrintArray(proc.Process(integers));

         Console.ReadKey();
      }

      private static void PrintArray(int[] array)
      {
         for (var i = 0; i < array.Length; i++)
         {
            Console.Write("{0}", array[i]);
            if (i != array.Length)
            {
               Console.Write(", ");
            }
         }

         Console.WriteLine();
      }
   }
}