/**
 * Необобщенный класс связного списка
 */

using System;

namespace LinkedListObjects
{
   static class Program
   {
      static void Main()
      {
         var objList = new LinkedList();
         objList.AddLast(2);
         objList.AddLast(4);
         objList.AddLast("6");

         try
         {
            foreach (int i in objList)
            {
               Console.WriteLine(i);
            }
         }
         catch (InvalidCastException invalidCastEx)
         {
            Console.WriteLine(invalidCastEx);
         }

         Console.ReadLine();
      }
   }
}
