/**
 * Абстракция для способов обхода контейнеров
 */

using static System.Console;

namespace Iterator
{
   internal static class Program
   {
      private static void Main()
      {
         IAggregator<string> strAggregator = new AggregatorImpl<string>();
         strAggregator[0] = "Item A";
         strAggregator[1] = "Item B";
         strAggregator[2] = "Item C";
         strAggregator[3] = "Item D";

         IIterator<string> iterator = new IteratorImpl<string>(strAggregator);

         WriteLine("Iterating over collection");

         var currentItem = iterator.First();
         while (currentItem != null)
         {
            WriteLine(currentItem);
            currentItem = iterator.Next();
         }

         ReadLine();
      }
   }
}