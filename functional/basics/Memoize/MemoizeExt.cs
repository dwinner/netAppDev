using System;
using System.Collections.Generic;

namespace _12_Memoize
{
   public static class MemoizeExt
   {
      /// <summary>
      ///    Memoize repeated calls
      /// </summary>
      /// <param name="self">Function to memoize</param>
      /// <typeparam name="TIn">Input parameter type</typeparam>
      /// <typeparam name="TOut">Return parameter type</typeparam>
      /// <returns></returns>
      public static Func<TIn, TOut> Memoize<TIn, TOut>(this Func<TIn, TOut> self)
      {
         IDictionary<TIn, TOut> cache = new Dictionary<TIn, TOut>(EqualityComparer<TIn>.Default);
         return input =>
         {
            if (cache.TryGetValue(input, out var result))
            {
               return result;
            }

            result = self(input);
            cache[input] = result;

            return result;
         };
      }
   }
}