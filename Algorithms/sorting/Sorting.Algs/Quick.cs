using static Sorting.Algs.SortHelpers;

namespace Sorting.Algs;

/// <summary>
///    The <tt>Quick</tt> class provides static methods for sorting an
///    array and selecting the ith smallest element in an array using quicksort
/// </summary>
public static class Quick
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">Array to be sorted</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(TItem[] anArray)
      where TItem : IComparable<TItem> =>
      Sort(anArray, 0, anArray.Length - 1);

   private static void Sort<TItem>(IList<TItem> anArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      if (highIdx <= lowIdx)
      {
         return;
      }

      var partitionIdx = GetPartitionIndex(anArray, lowIdx, highIdx);
      Sort(anArray, lowIdx, partitionIdx - 1);
      Sort(anArray, partitionIdx + 1, highIdx);
   }

   /// <summary>
   ///    Partition the subarray a[lo .. hi] by returning an index j
   ///    that a[lo .. j-1] is less or equal to a[j] is less or equal to a[j+1 .. hi]
   /// </summary>
   /// <param name="anArray">The array</param>
   /// <param name="lowIdx">Low index</param>
   /// <param name="highIdx">High index</param>
   /// <typeparam name="TItem">Item type</typeparam>
   /// <returns>Partition index</returns>
   private static int GetPartitionIndex<TItem>(IList<TItem> anArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      var i = lowIdx;
      var j = highIdx + 1;
      var item = anArray[lowIdx];
      while (true)
      {
         // find item on lo to swap
         while (Less(anArray[++i], item))
         {
            if (i == highIdx)
            {
               break;
            }
         }

         // find item on hi to swap
         while (Less(item, anArray[--j]))
         {
            if (j == lowIdx)
            {
               // redundant since a[lo] acts as sentinel
               break;
            }
         }

         // check if pointers cross
         if (i >= j)
         {
            break;
         }

         anArray.Exchange(i, j);
      }

      // put v = a[j] into position
      anArray.Exchange(lowIdx, j);

      // with a[lo .. j-1] <= a[j] <= a[j+1 .. hi]
      return j;
   }
}