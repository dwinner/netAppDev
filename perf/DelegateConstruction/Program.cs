using System;

namespace DelegateConstruction
{
   internal static class Program
   {
      private static int Add(int x, int y) => x + y;
      private static int DoOperation(MathOp op, int x, int y) => op(x, y);

      private static void Main()
      {
         // Examine the IL for these two loops

         // Option 1: Bad
         for (var i = 0; i < 10; i++)
         {
            Console.WriteLine(DoOperation(Add, 1, 2));
         }

         // Options 2: Good
         MathOp op = Add;
         for (var i = 0; i < 10; i++)
         {
            Console.WriteLine(DoOperation(op, 1, 2));
         }

         // Option 3: OK
         for (var i = 0; i < 10; i++)
         {
            Console.WriteLine(DoOperation((x, y) => Add(x, y), 1, 2));
         }

         // Option 4: Equivalent to Option 3
         for (var i = 0; i < 10; i++)
         {
            Console.WriteLine(DoOperation((x, y) => { return Add(x, y); }, 1, 2));
         }
      }

      private delegate int MathOp(int x, int y);
   }
}