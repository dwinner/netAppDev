namespace StringHandling.Sorting;

/// <summary>
///    The <see cref="Msd" /> class provides static methods for sorting an
///    array of extended ASCII strings or integers using MSD radix sort.
/// </summary>
public static class Msd
{
   private const int BitsPerByte = 8;
   private const int BitsPerInt = 32; // each Java int is 32 bits
   private const int Radix = 256; // extended ASCII alphabet size
   private const int Cutoff = 15; // cutoff to insertion sort

   /// <summary>
   ///    Rearranges the array of extended ASCII strings in ascending order.
   /// </summary>
   /// <param name="arrayToSort">The array to be sorted</param>
   public static void Sort(string[] arrayToSort)
   {
      var len = arrayToSort.Length;
      var aux = new string[len];
      Sort(arrayToSort, 0, len - 1, 0, aux);
   }

   // sort from a[lo] to a[hi], starting at the dth character
   private static void Sort(string[] arrayToSort, int lo, int hi, int idx, IList<string> aux)
   {
      // cutoff to insertion sort for small subarrays
      if (hi <= lo + Cutoff)
      {
         Insertion(arrayToSort, lo, hi, idx);
         return;
      }

      // compute frequency counts
      var count = new int[Radix + 2];
      for (var i = lo; i <= hi; i++)
      {
         var charAt = arrayToSort[i][idx]; // CharAt(arrayToSort[i], d);
         count[charAt + 2]++;
      }

      // transform counts to indices
      for (var r = 0; r < Radix + 1; r++)
      {
         count[r + 1] += count[r];
      }

      // distribute
      for (var i = lo; i <= hi; i++)
      {
         var charAt = arrayToSort[i][idx]; // CharAt(arrayToSort[i], idx);
         aux[count[charAt + 1]++] = arrayToSort[i];
      }

      // copy back
      for (var i = lo; i <= hi; i++)
      {
         arrayToSort[i] = aux[i - lo];
      }

      // recursively sort for each character (excludes sentinel -1)
      for (var r = 0; r < Radix; r++)
      {
         Sort(arrayToSort, lo + count[r], lo + count[r + 1] - 1, idx + 1, aux);
      }
   }

   // insertion sort a[lo..hi], starting at dth character
   private static void Insertion(IList<string> arrayToSort, int lo, int hi, int idx)
   {
      for (var i = lo; i <= hi; i++)
      {
         for (var j = i; j > lo && Less(arrayToSort[j], arrayToSort[j - 1], idx); j--)
         {
            Exchange(arrayToSort, j, j - 1);
         }
      }
   }

   // is v less than w, starting at character d
   private static bool Less(string firstStr, string secondStr, int idx)
   {
      // assert v.substring(0, d).equals(w.substring(0, d));
      for (var i = idx; i < Math.Min(firstStr.Length, secondStr.Length); i++)
      {
         if (firstStr[i] < secondStr[i])
         {
            return true;
         }

         if (firstStr[i] > secondStr[i])
         {
            return false;
         }
      }

      return firstStr.Length < secondStr.Length;
   }

   /// <summary>
   ///    Rearranges the array of 32-bit integers in ascending order.
   /// </summary>
   /// <param name="arrayToSort">The array to be sorted</param>
   public static void Sort(int[] arrayToSort)
   {
      var len = arrayToSort.Length;
      var aux = new int[len];
      Sort(arrayToSort, 0, len - 1, 0, aux);
   }

   // MSD sort from a[lo] to a[hi], starting at the dth byte
   private static void Sort(int[] arrayToSort, int lo, int hi, int idx, IList<int> aux)
   {
      // cutoff to insertion sort for small subarrays
      if (hi <= lo + Cutoff)
      {
         Insertion(arrayToSort, lo, hi);
         return;
      }

      // compute frequency counts (need R = 256)
      var count = new int[Radix + 1];
      const int mask = Radix - 1; // 0xFF;
      var shift = BitsPerInt - BitsPerByte * idx - BitsPerByte;
      for (var i = lo; i <= hi; i++)
      {
         var charAt = (arrayToSort[i] >> shift) & mask;
         count[charAt + 1]++;
      }

      // transform counts to indices
      for (var r = 0; r < Radix; r++)
      {
         count[r + 1] += count[r];
      }

      // for most significant byte, 0x80-0xFF comes before 0x00-0x7F
      if (idx == 0)
      {
         var shift1 = count[Radix] - count[Radix / 2];
         var shift2 = count[Radix / 2];
         count[Radix] = shift1 + count[1]; // to simplify recursive calls later
         for (var r = 0; r < Radix / 2; r++)
         {
            count[r] += shift1;
         }

         for (var r = Radix / 2; r < Radix; r++)
         {
            count[r] -= shift2;
         }
      }

      // distribute
      for (var i = lo; i <= hi; i++)
      {
         var charAt = (arrayToSort[i] >> shift) & mask;
         aux[count[charAt]++] = arrayToSort[i];
      }

      // copy back
      for (var i = lo; i <= hi; i++)
      {
         arrayToSort[i] = aux[i - lo];
      }

      // no more bits
      if (idx == 3)
      {
         return;
      }

      // special case for most significant byte
      if (idx == 0 && count[Radix / 2] > 0)
      {
         Sort(arrayToSort, lo, lo + count[Radix / 2] - 1, 1, aux);
      }

      // special case for other bytes
      if (idx != 0 && count[0] > 0)
      {
         Sort(arrayToSort, lo, lo + count[0] - 1, idx + 1, aux);
      }

      // recursively sort for each character
      // (could skip r = R/2 for d = 0 and skip r = R for d > 0)
      for (var r = 0; r < Radix; r++)
      {
         if (count[r + 1] > count[r])
         {
            Sort(arrayToSort, lo + count[r], lo + count[r + 1] - 1, idx + 1, aux);
         }
      }
   }

   // insertion sort a[lo..hi]
   private static void Insertion(IList<int> a, int lo, int hi)
   {
      for (var i = lo; i <= hi; i++)
      {
         for (var j = i; j > lo && a[j] < a[j - 1]; j--)
         {
            Exchange(a, j, j - 1);
         }
      }
   }

   private static void Exchange<T>(IList<T> array, int index1, int index2) =>
      (array[index1], array[index2]) = (array[index2], array[index1]);
}