/**
 * Обобщенная версия связного списка
 */

using System;

namespace LinkedListSample
{
   static class Program
   {
      static void Main()
      {
         var intLinkedList = new LinkedList<int>();
         intLinkedList.AddLast(1);
         intLinkedList.AddLast(3);
         intLinkedList.AddLast(5);

         foreach (int i in intLinkedList)
         {
            Console.WriteLine(i);
         }

         var strLinkedList = new LinkedList<string>();
         strLinkedList.AddLast("2");
         strLinkedList.AddLast("four");
         strLinkedList.AddLast("foo");

         foreach (string s in strLinkedList)
         {
            Console.WriteLine(s);
         }

         Console.ReadKey();
      }
   }
}
