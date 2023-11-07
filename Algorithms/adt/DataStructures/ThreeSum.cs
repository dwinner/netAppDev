namespace DataStructures;

/// <summary>
///    Try out alg. comparing
/// </summary>
public static class ThreeSum
{
   /// <summary>
   ///    Returns the number of triples (i, j, k) with i &lt; j &lt; k such that a[i] + a[j] + a[k] == 0
   /// </summary>
   /// <param name="anArray">Array to test</param>
   /// <remarks>This implementation uses a triply nested loop and takes proportional to N^3 where N is the number of integers</remarks>
   /// <returns>The number of triples (i, j, k) with i &lt; j &lt; k such that a[i] + a[j] + a[k] == 0</returns>
   public static int CountN3(int[] anArray)
   {
      var length = anArray.Length;
      int count = default;
      for (var i = 0; i < length; i++)
      {
         for (var j = i + 1; j < length; j++)
         {
            for (var k = j + 1; k < length; k++)
            {
               if (anArray[i] + anArray[j] + anArray[k] == 0)
               {
                  count++;
               }
            }
         }
      }

      return count;
   }

   /// <summary>
   ///    Returns the number of triples (i, j, k) with i &lt; j &lt; k such that a[i] + a[j] + a[k] == 0
   /// </summary>
   /// <param name="anArray">The array of integers</param>
   /// <remarks>
   ///    This implementation uses sorting and binary search and takes time proportional to N^2 log N, where N is the
   ///    number of integers
   /// </remarks>
   /// <returns>The number of triples (i, j, k) with i &lt; j &lt; k such that a[i] + a[j] + a[k] == 0</returns>
   public static int CountN2LogN(int[] anArray)
   {
      var length = anArray.Length;
      int count = default;
      for (var i = 0; i < length; i++)
      {
         for (var j = i + 1; j < length; j++)
         {
            var k = Array.BinarySearch(anArray, -(anArray[i] + anArray[j]), Comparer<int>.Default);
            if (k > j)
            {
               count++;
            }
         }
      }

      return count;
   }
}