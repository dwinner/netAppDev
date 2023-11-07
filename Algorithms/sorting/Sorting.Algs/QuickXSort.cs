namespace Sorting.Algs;

/// <summary>
///    Uses the Bentley-McIlroy 3-way partitioning scheme,
///    chooses the partitioning element using Tukey's ninther,
///    and cuts off to insertion sort.
/// </summary>
/// <remarks>
///    The <tt>QuickX</tt> class provides static methods for sorting an
///    array using an optimized version of quicksort (using Bentley-McIlroy
///    3-way partitioning, Tukey's ninther, and cutoff to insertion sort).
/// </remarks>
public static class QuickXSort
{
   private const int CutOff = 0x8; // cutoff to insertion sort, must be >= 1

   public static void Sort<T>(T[] anArray)
      where T : IComparable<T> =>
      Sort(anArray, 0, anArray.Length - 1);

   private static void Sort<T>(T[] anArray, int lowIdx, int highIdx)
      where T : IComparable<T>
   {
      var range = highIdx - lowIdx + 1;

      switch (range)
      {
         // cutoff to insertion sort
         case <= CutOff:
            InsertionSort(anArray, lowIdx, highIdx);
            return;

         // use median-of-3 as partitioning element
         case <= 40:
         {
            var median = Median3(anArray, lowIdx, lowIdx + range / 2, highIdx);
            Exchange(anArray, median, lowIdx);
            break;
         }

         // use Tukey ninther as partitioning element
         default:
         {
            var eps = range / 8;
            var mid = lowIdx + range / 2;
            var m1 = Median3(anArray, lowIdx, lowIdx + eps, lowIdx + eps + eps);
            var m2 = Median3(anArray, mid - eps, mid, mid + eps);
            var m3 = Median3(anArray, highIdx - eps - eps, highIdx - eps, highIdx);
            var ninther = Median3(anArray, m1, m2, m3);
            Exchange(anArray, ninther, lowIdx);
            break;
         }
      }

      // Bentley-McIlroy 3-way partitioning
      int i = lowIdx, j = highIdx + 1;
      int p = lowIdx, q = highIdx + 1;
      while (true)
      {
         var item = anArray[lowIdx];
         while (Less(anArray[++i], item))
         {
            if (i == highIdx)
            {
               break;
            }
         }

         while (Less(item, anArray[--j]))
         {
            if (j == lowIdx)
            {
               break;
            }
         }

         if (i >= j)
         {
            break;
         }

         Exchange(anArray, i, j);
         if (Equals(anArray[i], item))
         {
            Exchange(anArray, ++p, i);
         }

         if (Equals(anArray[j], item))
         {
            Exchange(anArray, --q, j);
         }
      }

      Exchange(anArray, lowIdx, j);

      i = j + 1;
      j -= 1;
      for (var k = lowIdx + 1; k <= p; k++)
      {
         Exchange(anArray, k, j--);
      }

      for (var k = highIdx; k >= q; k--)
      {
         Exchange(anArray, k, i++);
      }

      Sort(anArray, lowIdx, j);
      Sort(anArray, i, highIdx);
   }

   private static void InsertionSort<T>(IList<T> anArray, int lowIdx, int highIdx)
      where T : IComparable<T>
   {
      for (var i = lowIdx; i <= highIdx; i++)
      {
         for (var j = i;
              j > lowIdx && Less(anArray[j], anArray[j - 1]);
              j--)
         {
            Exchange(anArray, j, j - 1);
         }
      }
   }

   private static int Median3<T>(IReadOnlyList<T> anArray, int i, int j, int k)
      where T : IComparable<T> =>
      Less(anArray[i], anArray[j])
         ? Less(anArray[j], anArray[k])
            ? j
            : Less(anArray[i], anArray[k])
               ? k
               : i
         : Less(anArray[k], anArray[j])
            ? j
            : Less(anArray[k], anArray[i])
               ? k
               : i;

   private static bool Less<T>(T anItem1, T anItem2)
      where T : IComparable<T> =>
      anItem1.CompareTo(anItem2) < 0;

   private static bool Equals<T>(T anItem1, T anItem2)
      where T : IComparable<T> =>
      anItem1.CompareTo(anItem2) == 0;

   private static void Exchange<T>(IList<T> anArray, int i, int j) =>
      (anArray[i], anArray[j]) = (anArray[j], anArray[i]);
}