using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurrenceRelation
{
   public static class SequenceExt
   {
      public static IEnumerable<T> StartWith<T>(params T[] seeds) => new List<T>(seeds).AsEnumerable();

      public static IEnumerable<T> ThenFollow<T>(this IEnumerable<T> thisSequence, Func<T, T, T> rule)
         where T : IEquatable<T>
      {
         while (true)
         {
            var sequenceArray = thisSequence as T[] ?? thisSequence.ToArray();
            var last = sequenceArray[sequenceArray.Length - 1];
            var lastButOne = sequenceArray[sequenceArray.Length - 2];
            thisSequence = sequenceArray.Concat(new List<T> {rule(last, lastButOne)}.AsEnumerable());
            yield return rule(last, lastButOne);
         }
         // ReSharper disable once FunctionNeverReturns
      }

      public static IEnumerable<T> ThenFollow<T>(this IEnumerable<T> thisSequence, Func<T, T> rule) where T : IEquatable<T>
      {
         while (true)
         {
            var sequenceArray = thisSequence as T[] ?? thisSequence.ToArray();
            var last = sequenceArray.ElementAt(sequenceArray.Count()-1);
            thisSequence = sequenceArray.Concat(new List<T>() {rule(last)}).AsEnumerable();
            yield return rule(last);
         }
         // ReSharper disable once FunctionNeverReturns
      }
   }
}