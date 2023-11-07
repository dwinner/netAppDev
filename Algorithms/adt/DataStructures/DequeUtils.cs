namespace DataStructures;

internal static class DequeUtils
{
   internal static readonly Func<int, int, bool> SingleOrEmpty = (tail, head) => tail - head <= 2;
   internal static readonly Func<int, int> ValidHead = freeHead => freeHead + 1;
   internal static readonly Func<int, int> ValidTail = freeTail => freeTail - 1;
   internal static readonly Func<int, int, int> ShiftIndexFunc = (capacity, count) => (capacity - count) / 2;
}