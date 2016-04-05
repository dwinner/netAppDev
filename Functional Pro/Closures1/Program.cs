/**
 * Захваченные переменные и замыкания
 */

using System;

namespace _07_Closures
{
   class Program
   {
      static void Main(string[] args)
      {
         // Создать массив целых чисел
         int[] integers = new int[] { 1, 2, 3, 4 };

         Factor factor = new Factor(2);
         Processor proc = new Processor();
         proc.Strategy = factor.Multiplier;
         PrintArray(proc.Process(integers));
         proc.Strategy = factor.Adder;
         factor = null;
         PrintArray(proc.Process(integers));

         Console.ReadKey();
      }

      private static void PrintArray(int[] array)
      {
         for (int i = 0; i < array.Length; i++)
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

   public delegate int ProcStrategy(int x);

   public class Processor
   {
      public ProcStrategy Strategy { get; set; }

      public int[] Process(int[] array)
      {
         int[] result = new int[array.Length];
         for (int i = 0; i < array.Length; i++)
         {
            result[i] = Strategy(array[i]);
         }
         return result;
      }
   }

   public class Factor
   {
      private int factor;

      public Factor(int fact)
      {
         factor = fact;
      }

      public ProcStrategy Multiplier { get { return delegate(int x) { return x * factor; }; } }

      public ProcStrategy Adder { get { return delegate(int x) { return x + factor; }; } }
   }
}
