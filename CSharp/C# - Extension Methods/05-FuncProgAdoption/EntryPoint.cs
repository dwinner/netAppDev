/**
 * Функциональное программирование в C#
 */

using System;
using System.Collections.Generic;

namespace _05_FuncProgAdoption
{
   class EntryPoint
   {
      static void Main()
      {
         var listInts = new List<int> { 1, 2, 3, 4 };
         var linkList = MyList<int>.CreateList(listInts);
         foreach (var item in linkList.LinkListIterator())
         {
            Console.Write("{0}, ", item);
         }
         Console.WriteLine();

         var generalIterator = linkList.GeneralIterator(list => list.Tail == null, list => list.Tail);
         foreach (var item in generalIterator)
         {
            Console.Write("{0}, ", item);
         }
         Console.WriteLine();

         foreach (var item in linkList.Reverse1().LinkListIterator())
         {
            Console.Write("{0}, ", item);
         }
         Console.WriteLine();

         foreach (var item in linkList.Reverse1().LinkListIterator())
         {
            Console.Write("{0}, ", item);
         }
         Console.WriteLine();

         foreach (var item in linkList.Reverse1().LinkListIterator())
         {
            Console.Write("{0}, ", item);
         }
         Console.WriteLine();

         Console.ReadKey();
      }
   }
}
