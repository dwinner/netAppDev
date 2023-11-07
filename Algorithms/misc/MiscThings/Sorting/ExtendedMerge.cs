using System.Diagnostics;

namespace MiscThings.Sorting;

/// <summary>
///    The <see cref="ExtendedMerge" /> class provides static methods for sorting an
///    array using an optimized version of mergesort.
///    <p>
///       In the worst case, this implementation takes
///       Theta(<em>n</em> log <em>n</em>) time to sort an array of
///       length <em>n</em> (assuming comparisons take constant time).
///    </p>
///    <p>
///       This sorting algorithm is stable.
///       It uses Theta(<em>n</em>) extra memory (not including the input array).
///    </p>
/// </summary>
public static class ExtendedMerge
{
   private const int Cutoff = 7; // cutoff to insertion sort

   /// <summary>
   ///    Rearranges the array in ascending order, using the provided order.
   /// </summary>
   /// <typeparam name="TItem">Item type</typeparam>
   /// <param name="inArray">The array to be sorted</param>
   /// <param name="comparer">The comparator that defines the total order</param>
   public static void Sort<TItem>(TItem[] inArray, IComparer<TItem> comparer)
   {
      var auxCloned = (TItem[])inArray.Clone();
      Sort(auxCloned, inArray, 0, inArray.Length - 1, comparer);

#if DEBUG
      Debug.Assert(IsSorted(inArray, comparer));
#endif
   }

   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order.
   /// </summary>
   /// <typeparam name="TItem">Item type</typeparam>
   /// <param name="inArray">The array to be sorted</param>
   public static void Sort<TItem>(TItem[] inArray)
      where TItem : IComparable<TItem>
   {
      var auxCloned = (TItem[])inArray.Clone();
      Sort(auxCloned, inArray, 0, inArray.Length - 1);

#if DEBUG
      Debug.Assert(IsSorted(inArray));
#endif
   }

   private static void Merge<TItem>(IReadOnlyList<TItem> srcArray, TItem[] dstArray, int lowIdx, int middleIdx,
      int highIdx)
      where TItem : IComparable<TItem>
   {
#if DEBUG
      // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays
      Debug.Assert(IsSorted(srcArray, lowIdx, middleIdx));
      Debug.Assert(IsSorted(srcArray, middleIdx + 1, highIdx));
#endif

      int
         i = lowIdx,
         j = middleIdx + 1;
      for (var k = lowIdx; k <= highIdx; k++)
      {
         if (i > middleIdx)
         {
            dstArray[k] = srcArray[j++];
         }
         else if (j > highIdx)
         {
            dstArray[k] = srcArray[i++];
         }
         else if (Less(srcArray[j], srcArray[i]))
         {
            dstArray[k] = srcArray[j++]; // to ensure stability
         }
         else
         {
            dstArray[k] = srcArray[i++];
         }
      }

#if DEBUG
      // postcondition: dst[lo .. hi] is sorted subarray
      Debug.Assert(IsSorted(dstArray, lowIdx, highIdx));
#endif
   }

   private static void Sort<TItem>(TItem[] srcArray, TItem[] dstArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      // if (hi <= lo) return;
      if (highIdx <= lowIdx + Cutoff)
      {
         InsertionSort(dstArray, lowIdx, highIdx);
         return;
      }

      var middleIdx = lowIdx + (highIdx - lowIdx) / 2;
      Sort(dstArray, srcArray, lowIdx, middleIdx);
      Sort(dstArray, srcArray, middleIdx + 1, highIdx);
      if (!Less(srcArray[middleIdx + 1], srcArray[middleIdx]))
      {
         Array.Copy(srcArray, lowIdx, dstArray, lowIdx, highIdx - lowIdx + 1);
         return;
      }

      Merge(srcArray, dstArray, lowIdx, middleIdx, highIdx);
   }

   // sort from a[lo] to a[hi] using insertion sort
   private static void InsertionSort<TItem>(IList<TItem> inputArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      for (var i = lowIdx; i <= highIdx; i++)
      {
         for (var j = i;
              j > lowIdx && Less(inputArray[j], inputArray[j - 1]);
              j--)
         {
            Exch(inputArray, j, j - 1);
         }
      }
   }

   private static void Merge<TItem>(IReadOnlyList<TItem> srcArray, TItem[] dstArray, int lowIdx, int middleIdx,
      int highIdx, IComparer<TItem> comparer)
   {
#if DEBUG
      // precondition: src[lo .. mid] and src[mid+1 .. hi] are sorted subarrays
      Debug.Assert(IsSorted(srcArray, lowIdx, middleIdx, comparer));
      Debug.Assert(IsSorted(srcArray, middleIdx + 1, highIdx, comparer));
#endif

      int
         i = lowIdx,
         j = middleIdx + 1;
      for (var k = lowIdx; k <= highIdx; k++)
      {
         if (i > middleIdx)
         {
            dstArray[k] = srcArray[j++];
         }
         else if (j > highIdx)
         {
            dstArray[k] = srcArray[i++];
         }
         else if (Less(srcArray[j], srcArray[i], comparer))
         {
            dstArray[k] = srcArray[j++];
         }
         else
         {
            dstArray[k] = srcArray[i++];
         }
      }

#if DEBUG
      // postcondition: dst[lo .. hi] is sorted subarray
      Debug.Assert(IsSorted(dstArray, lowIdx, highIdx, comparer));
#endif
   }

   private static void Sort<TItem>(TItem[] srcArray, TItem[] dstArray, int lowIdx, int highIdx,
      IComparer<TItem> comparer)
   {
      // if (hi <= lo) return;
      if (highIdx <= lowIdx + Cutoff)
      {
         InsertionSort(dstArray, lowIdx, highIdx, comparer);
         return;
      }

      var middleIdx = lowIdx + (highIdx - lowIdx) / 2;
      Sort(dstArray, srcArray, lowIdx, middleIdx, comparer);
      Sort(dstArray, srcArray, middleIdx + 1, highIdx, comparer);
      if (!Less(srcArray[middleIdx + 1], srcArray[middleIdx], comparer))
      {
         Array.Copy(srcArray, lowIdx, dstArray, lowIdx, highIdx - lowIdx + 1);
         return;
      }

      Merge(srcArray, dstArray, lowIdx, middleIdx, highIdx, comparer);
   }

   // sort from a[lo] to a[hi] using insertion sort
   private static void InsertionSort<TItem>(IList<TItem> inputArray, int lowIdx, int highIdx, IComparer<TItem> comparer)
   {
      for (var i = lowIdx; i <= highIdx; i++)
      {
         for (var j = i;
              j > lowIdx && Less(inputArray[j], inputArray[j - 1], comparer);
              j--)
         {
            Exch(inputArray, j, j - 1);
         }
      }
   }

   // exchange a[i] and a[j]
   private static void Exch<TItem>(IList<TItem> inputArray, int idx1, int idx2) =>
      (inputArray[idx1], inputArray[idx2]) = (inputArray[idx2], inputArray[idx1]);

   // is a[i] < a[j]?
   private static bool Less<TItem>(TItem item1, TItem item2)
      where TItem : IComparable<TItem> =>
      item1.CompareTo(item2) < 0;

   // is a[i] < a[j]?
   private static bool Less<TItem>(TItem item1, TItem item2, IComparer<TItem> comparer) =>
      comparer.Compare(item1, item2) < 0;

#if DEBUG
   private static bool IsSorted<TItem>(IReadOnlyList<TItem> inputArray)
      where TItem : IComparable<TItem> =>
      IsSorted(inputArray, 0, inputArray.Count - 1);

   private static bool IsSorted<TItem>(IReadOnlyList<TItem> inputArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      for (var i = lowIdx + 1; i <= highIdx; i++)
      {
         if (Less(inputArray[i], inputArray[i - 1]))
         {
            return false;
         }
      }

      return true;
   }

   private static bool IsSorted<TItem>(IReadOnlyList<TItem> inputArray, IComparer<TItem> comparer) =>
      IsSorted(inputArray, 0, inputArray.Count - 1, comparer);

   private static bool IsSorted<TItem>(IReadOnlyList<TItem> inputArray, int lowIdx, int highIdx,
      IComparer<TItem> comparer)
   {
      for (var i = lowIdx + 1; i <= highIdx; i++)
      {
         if (Less(inputArray[i], inputArray[i - 1], comparer))
         {
            return false;
         }
      }

      return true;
   }
#endif
}