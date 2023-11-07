namespace Sorting.Algs;

/// <summary>
///    The <tt>Shell</tt> class provides static methods for sorting an
///    array using Shellsort with Knuth's increment sequence (1, 4, 13, 40, ...)
/// </summary>
public static class Shell
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">The array to be sorted</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray)
      where TItem : IComparable<TItem>
   {
      var length = anArray.Length;

      // 3x+1 increment sequence:  1, 4, 13, 40, 121, 364, 1093, ...
      var h = 1;
      while (h < length / 3)
      {
         h = 3 * h + 1;
      }

      while (h >= 1)
      {
         // h-sort the array
         for (var i = h; i < length; i++)
         {
            for (var j = i; j >= h && SortHelpers.Less(anArray[j], anArray[j - h]); j -= h)
            {
               anArray.Exchange(j, j - h);
            }
         }

         h /= 3;
      }
   }
}