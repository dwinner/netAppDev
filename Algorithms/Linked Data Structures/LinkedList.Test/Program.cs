using System;
using LinkedList.Lib;

namespace LinkedList.Test
{
   internal static class Program
   {
      private static void Main()
      {
         var list = new List<dynamic>();

         const char character = '$';
         const int integer = 34567;
         const string @string = "hello";

         list.InsertAtFront(true);
         Console.WriteLine(list);

         list.InsertAtFront(character);
         Console.WriteLine(list);

         list.InsertAtBack(integer);
         Console.WriteLine(list);

         list.InsertAtBack(@string);
         Console.WriteLine(list);

         try
         {
            object removedObject = list.RemoveFromFront();
            Console.WriteLine($"{removedObject} removed");
            Console.WriteLine(list);

            removedObject = list.RemoveFromFront();
            Console.WriteLine($"{removedObject} removed");
            Console.WriteLine(list);

            removedObject = list.RemoveFromBack();
            Console.WriteLine($"{removedObject} removed");
            Console.WriteLine(list);

            removedObject = list.RemoveFromBack();
            Console.WriteLine($"{removedObject} removed");
            Console.WriteLine(list);
         }
         catch (EmptyListException error)
         {
            Console.Error.WriteLine(error);
         }
      }
   }
}