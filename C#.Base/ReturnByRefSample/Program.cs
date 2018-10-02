/**
 * Return of objects by reference
 */

using System;
using static System.Console;

namespace ReturnByRefSample
{
   internal static class Program
   {
      private static void Main()
      {
         int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9};
         ref var r = ref Find(numbers, x => x > 4);
         WriteLine($"{r} == {numbers[4]}");
         r = 0;
         WriteLine($"{r} == {numbers[4]}");
      }

      private static ref int Find(int[] list, Func<int, bool> pred)
      {
         int i;
         for (i = 0; !pred(list[i]); i++) ;
         return ref list[i];
      }
   }
}