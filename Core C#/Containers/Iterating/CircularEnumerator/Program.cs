/**
 * Циклический перечислитель
 */

using System;
using System.Collections.Generic;

namespace _15_CircularEnumerator
{
   class Program
   {
      static void Main()
      {
         var intLinkedList = new LinkedList<int>();
         for (int i = 1; i < 6; i++)
         {
            intLinkedList.AddLast(i);
         }
         var circularIterator = new CircularIterator<int>(intLinkedList.First);
         int counter = 0;
         foreach (int n in circularIterator)
         {
            Console.WriteLine(n);
            if (counter++ == 100)
               circularIterator.Stop();
         }

         Console.ReadKey();
      }
   }
}
