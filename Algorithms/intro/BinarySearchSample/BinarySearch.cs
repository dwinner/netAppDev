namespace BinarySearchSample;

/// <summary>
///    The <tt>BinarySearch</tt> class provides a static method for binary
///    searching for an integer in a sorted array of integers.
///    <p>
///       The <em>rank</em> operations takes logarithmic time in the worst case.
///    </p>
/// </summary>
public static class BinarySearch
{
   /// <summary>
   ///    Searches for the integer key in the sorted array <paramref name="anArray" />
   /// </summary>
   /// <param name="aKey">The search key</param>
   /// <param name="anArray">The array of integers, must be sorted in ascending order</param>
   /// <returns>Index of key in array <paramref name="anArray" /> if present; -1 if not present</returns>
   public static int Rank(int aKey, int[] anArray)
   {
      var low = 0;
      var high = anArray.Length - 1;
      while (low <= high)
      {
         // Key is in [low..high] or not present
         var middle = low + (high - low) / 2;
         if (aKey < anArray[middle])
         {
            high = middle - 1;
         }
         else if (aKey > anArray[middle])
         {
            low = middle + 1;
         }
         else
         {
            return middle;
         }
      }

      return -1;
   }
}