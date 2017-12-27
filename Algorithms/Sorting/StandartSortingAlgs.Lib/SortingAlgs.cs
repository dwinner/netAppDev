using System;

namespace StandartSortingAlgs.Lib
{
   /// <summary>
   ///    Sorting algs
   /// </summary>
   public static class SortingAlgs
   {
      /// <summary>
      ///    Selection sort
      /// </summary>
      /// <typeparam name="T">Comparable generic type</typeparam>
      /// <param name="values">Array values</param>
      public static void SelectionSort<T>(T[] values)
         where T : IComparable<T>
      {
         for (var i = 0; i < values.Length - 1; i++)
         {
            var smallest = i;
            for (var j = i + 1; j < values.Length; j++)
               if (values[j].CompareTo(values[smallest]) < 0)
                  smallest = j;

            Swap(ref values[i], ref values[smallest]);
         }
      }

      /// <summary>
      ///    Insertion sort
      /// </summary>
      /// <typeparam name="T">Comparable generic type</typeparam>
      /// <param name="values">Array values</param>
      public static void InsertionSort<T>(T[] values)
         where T : IComparable<T>
      {
         // loop over data.Length - 1 elements
         for (var nextIndex = 1; nextIndex < values.Length; ++nextIndex)
         {
            // store value in current element
            var valueToInsert = values[nextIndex];

            // initialize location to place element
            var moveItemIndex = nextIndex;

            // search for place to put the current element
            while (moveItemIndex > 0 && values[moveItemIndex - 1].CompareTo(valueToInsert) > 0)
            {
               // shift element right one slot
               values[moveItemIndex] = values[moveItemIndex - 1];
               moveItemIndex--;
            }

            // place the inserted element
            values[moveItemIndex] = valueToInsert;
         }
      }

      private static void Swap<T>(ref T first, ref T second)
      {
         var temp = first;
         first = second;
         second = temp;
      }
   }
}