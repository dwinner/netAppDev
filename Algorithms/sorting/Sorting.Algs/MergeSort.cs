namespace Sorting.Algs;

/// <summary>
///    The <tt>Merge</tt> class provides static methods for sorting an
///    array using mergesort.
/// </summary>
public static class MergeSort
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">Array to be sorted</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(TItem[] anArray)
      where TItem : IComparable<TItem>
   {
      var auxArray = new TItem[anArray.Length];
      Sort(anArray, auxArray, 0, anArray.Length - 1);
   }

   /// <summary>
   ///    Mergesort a[lo..hi] using auxiliary array aux[lo..hi]
   /// </summary>
   /// <param name="anArray">Array to be sorted</param>
   /// <param name="anAuxArray">Auxilary array</param>
   /// <param name="lowIdx">Low index</param>
   /// <param name="highIdx">High index</param>
   /// <typeparam name="TItem">Item type</typeparam>
   private static void Sort<TItem>(IList<TItem> anArray, IList<TItem> anAuxArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      if (highIdx <= lowIdx)
      {
         return;
      }

      var mid = lowIdx + (highIdx - lowIdx) / 2;
      Sort(anArray, anAuxArray, lowIdx, mid);
      Sort(anArray, anAuxArray, mid + 1, highIdx);
      Merge(anArray, anAuxArray, lowIdx, mid, highIdx);
   }

   /// <summary>
   ///    Stably merge a[lo .. mid] with a[mid+1 ..hi] using aux[lo .. hi]
   /// </summary>
   /// <param name="anArray">Array to be sorted</param>
   /// <param name="anAuxArray">Auxilary array</param>
   /// <param name="lowIdx">Low index</param>
   /// <param name="middleIdx">Middle index</param>
   /// <param name="highIdx">High index</param>
   /// <typeparam name="TItem">Item type</typeparam>
   private static void Merge<TItem>(
      IList<TItem> anArray, IList<TItem> anAuxArray, int lowIdx, int middleIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      // copy to aux[]
      for (var k = lowIdx; k <= highIdx; k++)
      {
         anAuxArray[k] = anArray[k];
      }

      // merge back to a[]
      var i = lowIdx;
      var j = middleIdx + 1;
      for (var k = lowIdx; k <= highIdx; k++)
      {
         if (i > middleIdx)
         {
            anArray[k] = anAuxArray[j++];
         }
         else if (j > highIdx)
         {
            anArray[k] = anAuxArray[i++];
         }
         else if (SortHelpers.Less(anAuxArray[j], anAuxArray[i]))
         {
            anArray[k] = anAuxArray[j++];
         }
         else
         {
            anArray[k] = anAuxArray[i++];
         }
      }
   }
}