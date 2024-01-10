unsafe
{
   const int arrayLength = 16;

   var array1 = Enumerable.Range(0, arrayLength).Select(x => (byte)x).ToArray();
   var array2 = new byte[arrayLength];

   fixed (byte* p1 = array1)
   fixed (byte* p2 = array2)
   {
      // Our simplified version of MemCopy:
      MemCopy(p1, p2, arrayLength);

      // To see a real-world implementation of MemCopy, hit F12 on Buffer.MemoryCopy:
      Buffer.MemoryCopy(p1, p2, arrayLength, arrayLength);
   }

   return;

// Simplified implementation (assumes nicely aligned boundaries, numberOfBytes divisible by nint size)
   void MemCopy(void* source, void* dest, nuint numberOfBytes)
   {
      var nativeSource = (nint*)source;
      var nativeDest = (nint*)dest;

      // We want to copy memory one native word-length at a time - this is likely to be most efficient.
      // Hence we need to calculate the number of iterations = numberOfBytes / word-length.
      // nuint is the ideal type for this calculation.
      var iterations = numberOfBytes / (nuint)sizeof(nuint);

      // Because nativeSource and nativeDest are nint*, we will enumerate the memory in increments
      // of the native word size, which is exactly our goal.
      for (nuint i = 0; i < iterations; i++)
      {
         nativeDest[i] = nativeSource[i];
      }
   }
}