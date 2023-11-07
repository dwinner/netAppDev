using static Sorting.Algs.SortHelpers;

namespace Sorting.Algs;

/// <summary>
///    The <tt>Selection</tt> class provides static methods for sorting an
///    array using selection sort
/// </summary>
public static class SelectionSort
{
   /// <summary>
   ///    Selection sort
   /// </summary>
   /// <param name="anArray">Array</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray)
      where TItem : IComparable<TItem>
   {
      var arrayLen = anArray.Length;
      for (var i = 0; i < arrayLen; i++)
      {
         var min = i;
         for (var j = i + 1; j < arrayLen; j++)
         {
            if (Less(anArray[j], anArray[min]))
            {
               min = j;
            }
         }

         anArray.Exchange(i, min);
      }
   }

   /// <summary>
   ///    Rearranges the array in ascending order, using a comparator
   /// </summary>
   /// <param name="anArray">The array</param>
   /// <param name="aComparer">The comparator specifying the order</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray, in IComparer<TItem> aComparer)
   {
      var len = anArray.Length;
      for (var i = 0; i < len; i++)
      {
         var min = i;
         for (var j = i + 1; j < len; j++)
         {
            if (Less(anArray[j], anArray[min], aComparer))
            {
               min = j;
            }
         }

         anArray.Exchange(i, min);
      }
   }

   /// <summary>
   ///    Rearranges the array in ascending order, using a comparator
   /// </summary>
   /// <param name="anArray">The array</param>
   /// <param name="aCompFunc">Function to compare</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Sort<TItem>(in TItem[] anArray, in Comparison<TItem> aCompFunc)
   {
      var len = anArray.Length;
      for (var i = 0; i < len; i++)
      {
         var min = i;
         for (var j = i + 1; j < len; j++)
         {
            if (Less(anArray[j], anArray[min], aCompFunc))
            {
               min = j;
            }
         }

         anArray.Exchange(i, min);
      }
   }
}