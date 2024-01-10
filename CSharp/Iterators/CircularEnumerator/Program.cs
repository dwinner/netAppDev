/**
 * Циклический перечислитель
 */

using System;
using System.Collections.Generic;

namespace _15_CircularEnumerator
{
   internal class Program
   {
      private static void Main()
      {
         var intLinkedList = new LinkedList<int>();
         for (var i = 1; i < 6; i++)
         {
            intLinkedList.AddLast(i);
         }

         var circularIterator = new CircularIterator<int>(intLinkedList.First);
         var counter = 0;
         foreach (var n in circularIterator)
         {
            Console.WriteLine(n);
            if (counter++ == 100)
            {
               circularIterator.Stop();
            }
         }

         Console.ReadKey();
      }
   }
}