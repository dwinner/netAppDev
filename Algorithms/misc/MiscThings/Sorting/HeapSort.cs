namespace MiscThings.Sorting;

/// <summary>
///    The <see cref="HeapSort" /> class provides a static method to sort an array
///    using <em>heapsort</em>.
///    <p>
///       This implementation takes Theta(<em>n</em> log <em>n</em>) time
///       to sort any array of length <em>n</em> (assuming comparisons
///       take constant time). It makes at most
///       2 <em>n</em> log<sub>2</sub> <em>n</em> compares.
///    </p>
///    <p>
///       This sorting algorithm is not stable.
///       It uses Theta(1) extra memory (not including the input array).
///    </p>
/// </summary>
public static class HeapSort
{
   /// <summary>
   ///    Rearranges the array in ascending order, using the natural order.
   /// </summary>
   /// <typeparam name="TItem">Item array</typeparam>
   /// <param name="inputArray">The array to be sorted</param>
   public static void Sort<TItem>(TItem[] inputArray)
      where TItem : IComparable<TItem>
   {
      var arrayLen = inputArray.Length;
      int k;

      // heapify phase
      for (k = arrayLen / 2; k >= 1; k--)
      {
         Sink(inputArray, k, arrayLen);
      }

      // sortdown phase
      k = arrayLen;
      while (k > 1)
      {
         Exch(inputArray, 1, k--);
         Sink(inputArray, 1, k);
      }
   }

   private static void Sink<TItem>(TItem[] inputArray, int k, int n)
      where TItem : IComparable<TItem>
   {
      while (2 * k <= n)
      {
         var j = 2 * k;
         if (j < n && Less(inputArray, j, j + 1))
         {
            j++;
         }

         if (!Less(inputArray, k, j))
         {
            break;
         }

         Exch(inputArray, k, j);
         k = j;
      }
   }

   private static bool Less<TItem>(IReadOnlyList<TItem> inputArray, int idx1, int idx2)
      where TItem : IComparable<TItem> =>
      inputArray[idx1 - 1].CompareTo(inputArray[idx2 - 1]) < 0;

   private static void Exch<TItem>(IList<TItem> inputArray, int idx1, int idx2) =>
      (inputArray[idx1 - 1], inputArray[idx2 - 1]) = (inputArray[idx2 - 1], inputArray[idx1 - 1]);
}