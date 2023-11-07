using System.Diagnostics;

namespace MiscThings.Sorting;

/// <summary>
///    The <see cref="BottomUpMerge" /> class provides static methods for sorting an
///    array using <em>bottom-up mergesort</em>. It is non-recursive.
///    <p>
///       This implementation takes Theta(<em>n</em> log <em>n</em>) time
///       to sort any array of length <em>n</em> (assuming comparisons
///       take constant time). It makes between <em>n</em> log<sub>2</sub> <em>n</em> and
///       ~ 1 <em>n</em> log<sub>2</sub> <em>n</em> compares.
///    </p>
///    <p>
///       This sorting algorithm is stable.
///       It uses Theta(<em>n</em>) extra memory (not including the input array).
///    </p>
/// </summary>
public static class BottomUpMerge
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order.
   /// </summary>
   /// <typeparam name="TItem">Item type</typeparam>
   /// <param name="inputArray">the array to be sorted</param>
   public static void Sort<TItem>(TItem[] inputArray)
      where TItem : IComparable<TItem>
   {
      var arrayLen = inputArray.Length;
      var aux = new TItem[arrayLen];
      for (var len = 1; len < arrayLen; len *= 2)
      {
         for (var lowIdx = 0;
              lowIdx < arrayLen - len;
              lowIdx += len + len)
         {
            var middleIdx = lowIdx + len - 1;
            var highIdx = Math.Min(lowIdx + len + len - 1, arrayLen - 1);
            Merge(inputArray, aux, lowIdx, middleIdx, highIdx);
         }
      }

#if DEBUG
      Debug.Assert(IsSorted(inputArray));
#endif
   }

   // stably merge a[lo..mid] with a[mid+1..hi] using aux[lo..hi]
   private static void Merge<TItem>(
      IList<TItem> inputArray, IList<TItem> auxArray, int lowIdx, int middleIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      // copy to aux[]
      for (var k = lowIdx; k <= highIdx; k++)
      {
         auxArray[k] = inputArray[k];
      }

      // merge back to a[]
      int
         i = lowIdx,
         j = middleIdx + 1;
      for (var k = lowIdx; k <= highIdx; k++)
      {
         if (i > middleIdx)
         {
            inputArray[k] = auxArray[j++]; // this copying is unnecessary
         }
         else if (j > highIdx)
         {
            inputArray[k] = auxArray[i++];
         }
         else if (Less(auxArray[j], auxArray[i]))
         {
            inputArray[k] = auxArray[j++];
         }
         else
         {
            inputArray[k] = auxArray[i++];
         }
      }
   }

   // is v < w ?
   private static bool Less<TItem>(TItem item1, TItem item2)
      where TItem : IComparable<TItem> =>
      item1.CompareTo(item2) < 0;

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