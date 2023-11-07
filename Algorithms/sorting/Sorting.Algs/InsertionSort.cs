using static Sorting.Algs.SortHelpers;

namespace Sorting.Algs;

/// <summary>
///    The <tt>Insertion</tt> class provides static methods for sorting an
///    array using insertion sort
/// </summary>
public static class InsertionSort
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">The array to be sorted</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray)
      where TItem : IComparable<TItem>
   {
      var len = anArray.Length;
      for (var i = 0; i < len; i++)
      {
         for (var j = i; j > 0 && Less(anArray[j], anArray[j - 1]); j--)
         {
            anArray.Exchange(j, j - 1);
         }
      }
   }

   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order
   /// </summary>
   /// <param name="anArray">The array to be sorted</param>
   /// <param name="aComparer">Comparer to apply</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray, in IComparer<TItem> aComparer)
   {
      var len = anArray.Length;
      for (var i = 0; i < len; i++)
      {
         for (var j = i; j > 0 && Less(anArray[j], anArray[j - 1], aComparer); j--)
         {
            anArray.Exchange(j, j - 1);
         }
      }
   }
}