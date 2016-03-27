using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericMethods
{
   public static class Algorithm
   {
      public static decimal Accumulate(IEnumerable<IAccount> source)
      {
         return source.Sum(account => account.Balance);
      }

      public static decimal Accumulate<TAccount>(IEnumerable<TAccount> source)
         where TAccount : IAccount
      {
         return source.Sum(account => account.Balance);
      }

      public static TOut Accumulate<TIn, TOut>(IEnumerable<TIn> source, Func<TIn, TOut, TOut> action)
      {
         TOut sum = default (TOut);
         return source.Aggregate(sum, (current, item) => action(item, current));
      }
   }
}