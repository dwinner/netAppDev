namespace StringHandling.Sorting;

/// <summary>
///    The <see cref="Lsd" /> class provides static methods for sorting an
///    array of <em>w</em>-character strings or 32-bit integers using LSD radix sort.
/// </summary>
public static class Lsd
{
   private const int BitsPerByte = 8;

   /// <summary>
   ///    Rearranges the array of w-character strings in ascending order.
   /// </summary>
   /// <param name="anArray">The array to be sorted</param>
   /// <param name="fixedStrLen">The number of characters per string</param>
   public static void Sort(string[] anArray, int fixedStrLen)
   {
      var len = anArray.Length;
      const int radix = 0x100; // extend ASCII alphabet size
      var aux = new string[len];

      for (var dIdx = fixedStrLen - 1; dIdx >= 0; dIdx--)
      {
         // sort by key-indexed counting on dIdx'th character

         // compute frequency counts
         var count = new int[radix + 1];
         for (var i = 0; i < len; i++)
         {
            var incChar = anArray[i][dIdx];
            count[incChar + 1]++;
         }

         // compute cumulates
         for (var rIdx = 0; rIdx < radix; rIdx++)
         {
            count[rIdx + 1] += count[rIdx];
         }

         // move data
         for (var i = 0; i < len; i++)
         {
            var charAtIdx = anArray[i][dIdx];
            aux[count[charAtIdx]++] = anArray[i];
         }

         // copy back
         Array.Copy(aux, anArray, len);
      }
   }

   /// <summary>
   ///    Rearranges the array of 32-bit integers in ascending order.
   /// </summary>
   /// <remarks>This is about 2-3x faster than Arrays.sort().</remarks>
   /// <param name="anArray">The array to be sorted</param>
   public static void Sort(int[] anArray)
   {
      const int bits = 0x20; // each int is 32 bits
      const int radix = 1 << BitsPerByte; // each byte is between 0 and 255
      const int mask = radix - 1; // 0xFF
      const int octet = bits / BitsPerByte; // each int is 4 bytes

      var len = anArray.Length;
      var aux = new int[len];

      for (var dIdx = 0; dIdx < octet; dIdx++)
      {
         // compute frequency counts
         var count = new int[radix + 1];
         for (var i = 0; i < len; i++)
         {
            var charToCount = (anArray[i] >> (BitsPerByte * dIdx)) & mask;
            count[charToCount + 1]++;
         }

         // compute cumulates
         for (var rIdx = 0; rIdx < radix; rIdx++)
         {
            count[rIdx + 1] += count[rIdx];
         }

         // for most significant byte, 0x80-0xFF comes before 0x00-0x7F
         if (dIdx == octet - 1)
         {
            var shift1 = count[radix] - count[radix / 2];
            var shift2 = count[radix / 2];
            for (var rIdx = 0; rIdx < radix / 2; rIdx++)
            {
               count[rIdx] += shift1;
            }

            for (var rIdx = radix / 2; rIdx < radix; rIdx++)
            {
               count[rIdx] -= shift2;
            }
         }

         // move data
         for (var i = 0; i < len; i++)
         {
            var charToCount = (anArray[i] >> (BitsPerByte * dIdx)) & mask;
            aux[count[charToCount]++] = anArray[i];
         }

         // copy back
         Array.Copy(aux, anArray, len);
      }
   }
}