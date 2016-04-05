/**  
 * Несколько точек входа
 */
using System;

namespace ManyMain
{
   static class Program
   {
      public static void Main(string[] args)
      {
         Console.Write("Press any key to continue . . . ");
         Console.ReadKey(true);
      }
   }

   class MathExample
   {
      static int Add(int x, int y)
      {
         return x + y;
      }

      public static int Main()
      {
         int i = Add(5, 10);
         Console.WriteLine(i);
         return 0;
      }
   }
}