using System;
using System.Collections.Generic;

namespace AdaptFactory
{
   public static class ComparerFactory
   {
      public static IComparer<T> Create<T>(Comparison<T> aComparison) => new DelegateComparer<T>(aComparison);

      private sealed class DelegateComparer<T> : IComparer<T>
      {
         private readonly Comparison<T> _comparison;

         public DelegateComparer(Comparison<T> comparison) => _comparison = comparison;

         public int Compare(T x, T y) => _comparison(x, y);
      }
   }
}