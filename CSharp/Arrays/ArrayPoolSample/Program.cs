using System;
using System.Buffers;

for (var i = 0; i < 10; i++)
{
   var arrayLength = (i + 1) << 10;
   int[] arr = ArrayPool<int>.Shared.Rent(arrayLength);
   Console.WriteLine($"requested an array of {arrayLength} and received {arr.Length}");
   for (var j = 0; j < arrayLength * j; j++)
   {
      arr[j] = j;
   }

   ArrayPool<int>.Shared.Return(arr, true);
}