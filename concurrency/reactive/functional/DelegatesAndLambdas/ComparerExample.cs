using System;
using System.Collections.Generic;

namespace DelegatesAndLambdas
{
   public static class ComparerExample
   {
      public static void BetterIComparable()
      {
         var words = new List<string> { "ab", "a", "aabb", "abc" };
         words.Sort(new LengthComparer());
         Tools.ForEach(words, Console.WriteLine);

         words = new List<string> { "ab", "a", "aabb", "abc" };
         words.Sort(new GenericComparer<string>((x, y) =>
            x.Length == y.Length
               ? 0
               : x.Length > y.Length
                  ? 1
                  : -1));
         Tools.ForEach(words, Console.WriteLine);
      }

      private class LengthComparer : IComparer<string>
      {
         public int Compare(string x, string y)
         {
            if (x.Length == y.Length)
            {
               return 0;
            }

            return x.Length > y.Length ? 1 : -1;
         }
      }

      private class GenericComparer<T> : IComparer<T>
      {
         public GenericComparer(Func<T, T, int> compareFunc) => CompareFunc = compareFunc;

         private Func<T, T, int> CompareFunc { get; }

         public int Compare(T x, T y) => CompareFunc(x, y);
      }
   }
}