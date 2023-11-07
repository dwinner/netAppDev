namespace Sorting.Algs;

/// <summary>
///    Sort helper functions
/// </summary>
public static class SortHelpers
{
   /// <summary>
   ///    Exchanges items in array
   /// </summary>
   /// <param name="self">Array</param>
   /// <param name="anIndex1">1st index</param>
   /// <param name="anIndex2">2nd index</param>
   /// <typeparam name="TItem">Item type</typeparam>
   public static void Exchange<TItem>(this IList<TItem> self, int anIndex1, int anIndex2) =>
      (self[anIndex1], self[anIndex2]) = (self[anIndex2], self[anIndex1]);

   public static bool Less<TItem>(TItem aFirst, TItem aSecond)
      where TItem : IComparable<TItem> =>
      aFirst.CompareTo(aSecond) < 0;

   public static bool Less<TItem>(TItem anItem1, TItem anItem2, IComparer<TItem> aComparer) =>
      aComparer.Compare(anItem1, anItem2) < 0;

   public static bool Less<TItem>(TItem anItem1, TItem anItem2, Comparison<TItem> compFunc) =>
      compFunc(anItem1, anItem2) < 0;

   public static bool IsSorted<TItem>(this TItem[] self)
      where TItem : IComparable<TItem>
   {
      for (var i = 1; i < self.Length; i++)
      {
         if (Less(self[i], self[i - 1]))
         {
            return false;
         }
      }

      return true;
   }

   public static bool IsSorted<TItem>(this TItem[] self, IComparer<TItem> aComparer)
   {
      for (var i = 1; i < self.Length; i++)
      {
         if (Less(self[i], self[i - 1], aComparer))
         {
            return false;
         }
      }

      return true;
   }

   public static bool IsSorted<TItem>(this TItem[] self, Comparison<TItem> compFunc)
   {
      for (var i = 1; i < self.Length; i++)
      {
         if (Less(self[i], self[i - 1], compFunc))
         {
            return false;
         }
      }

      return true;
   }
}