/**
 * Абстракция для способов обхода контейнеров
 */

using System;

namespace Iterator
{
   static class Program
   {
      static void Main()
      {
         IAggregator<string> strAggregator = new AggregatorImpl<string>();
         strAggregator[0] = "Item A";
         strAggregator[1] = "Item B";
         strAggregator[2] = "Item C";
         strAggregator[3] = "Item D";

         IIterator<string> iterator = new IteratorImpl<string>(strAggregator);

         Console.WriteLine("Iterating over collection");

         string currentItem = iterator.First();
         while (currentItem != null)
         {
            Console.WriteLine(currentItem);
            currentItem = iterator.Next();
         }

         Console.ReadLine();
      }
   }
}
