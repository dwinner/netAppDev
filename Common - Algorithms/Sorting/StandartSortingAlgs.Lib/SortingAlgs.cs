using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

      #region Merge sorting

      public static void MergeSort<T>(T[] values) where T : IComparable<T> => SortArray(values, 0, values.Length - 1);

      /// <summary>
      ///    Split array, sort subarrays and merge subarrays into sorted array
      /// </summary>
      /// <typeparam name="T">Comparable generic type</typeparam>
      /// <param name="values">Array values</param>
      /// <param name="low">Low index</param>
      /// <param name="high">High index</param>
      /// <param name="isParallel">true, if you want to apply sorting in parallel mode</param>
      private static void SortArray<T>(T[] values, int low, int high, bool isParallel = false)
         where T : IComparable<T>
      {
         if (high - low >= 1)
         {
            var middle1 = (low + high) / 2; // calculate the middle of array
            var middle2 = middle1 + 1; // calculate the next element over

            // split array in half; sort each half (recursive calls)
            if (isParallel)
            {
               var leftHalfArrayTask = Task.Run(() => SortArray(values, low, middle1));
               var rigthHalfArrayTask = Task.Run(() => SortArray(values, middle2, high));
               Task.WaitAll(leftHalfArrayTask, rigthHalfArrayTask);
            }
            else
            {
               SortArray(values, low, middle1); // first half of array
               SortArray(values, middle2, high); // second half of array
            }

            // merge two sorted arrays after split calls return
            Merge(values, low, middle1, middle2, high);
         }
      }

      private static void Merge<T>(IList<T> values, int left, int middle1, int middle2, int right)
         where T : IComparable<T>
      {
         var leftIndex = left; // index into left subarray
         var rightIndex = middle2; // index into right subarray
         var combinedIndex = left; // index into temporary working array
         var combined = new T[values.Count];

         // merge arrays until reaching end of either
         while (leftIndex <= middle1 && rightIndex <= right)
            // place smaller of two current elements into result and move to the next space in arrays
            combined[combinedIndex++] = values[leftIndex].CompareTo(values[rightIndex]) <= 0
               ? values[leftIndex++]
               : values[rightIndex++];

         // if left array is empty
         if (leftIndex == middle2)
            while (rightIndex <= right)
               combined[combinedIndex++] = values[rightIndex++];
         else // right array is empty
            while (leftIndex <= middle1)
               combined[combinedIndex++] = values[leftIndex++];

         // copy values back into original array
         for (var i = left; i <= right; ++i) values[i] = combined[i];
      }

      #endregion
   }
}