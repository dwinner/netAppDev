using System;
using System.Threading.Tasks;

namespace LockFreeStack
{
   internal static class Program
   {
      private const int NumItems = 10000000;

      private static void Main()
      {
         var stack = new LockFreeStack<int>();

         Parallel.For(0, NumItems,
            i => stack.Push(i));
         Console.WriteLine("Count: {0}", stack.Count);

         Parallel.For(0, NumItems,
            i => stack.Pop());
         Console.WriteLine("Count: {0}", stack.Count);
      }
   }
}