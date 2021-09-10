/**
 * Слабые ссылки
 */

using System;

namespace WeakReference
{
   internal static class Program
   {
      private static void Main()
      {
         // Создать слабую ссылку на объект MathTest
         var weakReference = new System.WeakReference(new MathTest());
         var math = weakReference.Target as MathTest;
         if (math != null)
         {
            math.Value = 30;
            Console.WriteLine("Value field of math variable contains {0}", math.Value);
            Console.WriteLine("Square of 30 is {0}", math.GetSquare(30));
         }
         else
         {
            Console.WriteLine("Reference is not available"); // Ссылка стала недоступна
         }

         GC.Collect();
         if (weakReference.IsAlive)
         {
            // ReSharper disable once RedundantAssignment
            math = weakReference.Target as MathTest;
         }
         else
         {
            Console.WriteLine("Reference is not available");
         }

         Console.ReadKey();
      }
   }

   internal class MathTest
   {
      public int Value { get; set; }

      public int GetSquare(int i) => (int) Math.Pow(i, 2);
   }
}