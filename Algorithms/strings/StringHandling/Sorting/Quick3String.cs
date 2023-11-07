using System.Diagnostics;

namespace StringHandling.Sorting;

/// <summary>
///    The <see cref="Quick3String" /> class provides static methods for sorting an
///    array of strings using 3-way radix quicksort.
/// </summary>
public static class Quick3String
{
   private const int Cutoff = 15; // cutoff to insertion sort
   private static readonly Random Rng = new(Environment.TickCount);

   /// <summary>
   ///    Rearranges the array of strings in ascending order.
   /// </summary>
   /// <param name="array">The array to be sorted</param>
   public static void Sort(string[] array)
   {
      Rng.Shuffle(array);
      Sort(array, 0, array.Length - 1, 0);
#if DEBUG
      Debug.Assert(IsSorted(array));
#endif
   }

   // 3-way string quicksort a[lo..hi] starting at dth character
   private static void Sort(IList<string> array, int lo, int hi, int idx)
   {
      // cutoff to insertion sort for small subarrays
      if (hi <= lo + Cutoff)
      {
         Insertion(array, lo, hi, idx);
         return;
      }

      int lt = lo, gt = hi;
      int subArray = array[lo][idx];
      var i = lo + 1;
      while (i <= gt)
      {
         int t = array[i][idx];
         if (t < subArray)
         {
            Exchange(array, lt++, i++);
         }
         else if (t > subArray)
         {
            Exchange(array, i, gt--);
         }
         else
         {
            i++;
         }
      }

      // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi].
      Sort(array, lo, lt - 1, idx);
      if (subArray >= 0)
      {
         Sort(array, lt, gt, idx + 1);
      }

      Sort(array, gt + 1, hi, idx);
   }

   // sort from a[lo] to a[hi], starting at the dth character
   private static void Insertion(IList<string> array, int lo, int hi, int idx)
   {
      for (var i = lo; i <= hi; i++)
      {
         for (var j = i; j > lo && Less(array[j], array[j - 1], idx); j--)
         {
            Exchange(array, j, j - 1);
         }
      }
   }

   // exchange a[i] and a[j]
   private static void Exchange(IList<string> array, int i, int j) => (array[i], array[j]) = (array[j], array[i]);

   // is v less than w, starting at character d
   private static bool Less(string str1, string str2, int idx)
   {
#if DEBUG
      Debug.Assert(str1[..idx].Equals(str2[..idx]));
#endif
      for (var i = idx; i < Math.Min(str1.Length, str2.Length); i++)
      {
         if (str1[i] < str2[i])
         {
            return true;
         }

         if (str1[i] > str2[i])
         {
            return false;
         }
      }

      return str1.Length < str2.Length;
   }

   // is the array sorted
   private static bool IsSorted(IReadOnlyList<string> array)
   {
      for (var i = 1; i < array.Count; i++)
      {
         if (string.Compare(array[i], array[i - 1], StringComparison.Ordinal) < 0)
         {
            return false;
         }
      }

      return true;
   }
}