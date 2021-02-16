using LinkedList.Lib;
using StackInheritance.Lib;
using static System.Console;

namespace StackInheritance.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var stack = new StackInheritance<dynamic>();

         // Create objects to store in the stack
         const char character = '$';
         const int integer = 34567;
         const string str = "hello";

         // Add items to the stack
         stack.Push(true);
         WriteLine(stack);
         stack.Push(character);
         WriteLine(stack);
         stack.Push(integer);
         WriteLine(stack);
         stack.Push(str);
         WriteLine(stack);

         // remove items from the stack
         try
         {
            while (true)
            {
               var removedObject = stack.Pop();
               WriteLine($"{removedObject} popped");
               WriteLine(stack);
            }
         }
         catch (EmptyListException emptyListEx)
         {
            Error.WriteLine(emptyListEx.StackTrace);
         }
      }
   }
}