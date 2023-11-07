namespace Sorting.Algs;

/// <summary>
///    The <tt>Quick3way</tt> class provides static methods for sorting an
///    array using quicksort with 3-way partitioning
/// </summary>
public static class Quick3Way
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">The array to be sorted</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(TItem[] anArray)
      where TItem : IComparable<TItem> =>
      Sort(anArray, 0, anArray.Length - 1);

   // quicksort the subarray a[lo .. hi] using 3-way partitioning
   private static void Sort<TItem>(IList<TItem> anArray, int lowIdx, int highIdx)
      where TItem : IComparable<TItem>
   {
      if (highIdx <= lowIdx)
      {
         return;
      }

      var lt = lowIdx;
      var gt = highIdx;
      var item = anArray[lowIdx];
      var i = lowIdx;
      while (i <= gt)
      {
         var cmpResult = anArray[i].CompareTo(item);
         switch (cmpResult)
         {
            case < 0:
               anArray.Exchange(lt++, i++);
               break;

            case > 0:
               anArray.Exchange(i, gt--);
               break;

            default:
               i++;
               break;
         }
      }

      // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
      Sort(anArray, lowIdx, lt - 1);
      Sort(anArray, gt + 1, highIdx);
   }
}