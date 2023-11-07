using System.Diagnostics;

namespace MiscThings.Sorting;

/// <summary>
///    The <see cref="InsertionX" />  class provides static methods for sorting
///    an array using an optimized version of insertion sort (with half exchanges
///    and a sentinel).
/// </summary>
public static class InsertionX
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order.
   /// </summary>
   /// <typeparam name="TItem">Item type</typeparam>
   /// <param name="inputArray">The array to be sorted</param>
   public static void Sort<TItem>(TItem[] inputArray)
      where TItem : IComparable<TItem>
   {
      var arrayLen = inputArray.Length;

      // put the smallest element in position to serve as sentinel
      var exchanges = 0;
      for (var i = arrayLen - 1; i > 0; i--)
      {
         if (Less(inputArray[i], inputArray[i - 1]))
         {
            Exch(inputArray, i, i - 1);
            exchanges++;
         }
      }

      if (exchanges == 0)
      {
         return;
      }

      // insertion sort with half-exchanges
      for (var i = 2; i < arrayLen; i++)
      {
         var item = inputArray[i];
         var j = i;
         while (Less(item, inputArray[j - 1]))
         {
            inputArray[j] = inputArray[j - 1];
            j--;
         }

         inputArray[j] = item;
      }

#if DEBUG
      Debug.Assert(IsSorted(inputArray));
#endif
   }

   private static bool Less<T>(T item1, T item2)
      where T : IComparable<T> =>
      item1.CompareTo(item2) < 0;

   private static void Exch<TItem>(IList<TItem> a, int idx1, int idx2) =>
      (a[idx1], a[idx2]) = (a[idx2], a[idx1]);

   private static bool IsSorted<TItem>(IReadOnlyList<TItem> inputArray)
      where TItem : IComparable<TItem>
   {
      for (var i = 1; i < inputArray.Count; i++)
      {
         if (Less(inputArray[i], inputArray[i - 1]))
         {
            return false;
         }
      }

      return true;
   }
}