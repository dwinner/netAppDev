using System;

namespace DelegateConstruction
{
    class Program
    {
        private delegate int MathOp(int x, int y);

        private static int Add(int x, int y) { return x + y; }
        private static int DoOperation(MathOp op, int x, int y) { return op(x, y); }

        static void Main(string[] args)
        {            
            // Examine the IL for these two loops

            // Option 1: Bad
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation(Add, 1, 2));
            }

            // Options 2: Good
            MathOp op = Add;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation(op, 1, 2));
            }

            // Option 3: OK
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation((x,y) => Add(x,y), 1, 2));
            }

            // Option 4: Equivalent to Option 3
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DoOperation((x,y) => { return Add(x, y); }, 1, 2));                
            }
        }
    }
}
