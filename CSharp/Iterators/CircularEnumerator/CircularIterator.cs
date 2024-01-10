using System;
using System.Collections;
using System.Collections.Generic;

namespace _15_CircularEnumerator
{
   public class CircularIterator<T> : IEnumerable<T>
   {
      private readonly IEnumerator<T> _enumerator;
      private readonly Type _enumType;

      public CircularIterator(LinkedListNode<T> start)
      {
         _enumerator = CreateEnumerator(start, false).GetEnumerator();
         _enumType = _enumerator.GetType();
      }

      public IEnumerator<T> GetEnumerator() => _enumerator;

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

      private static IEnumerable<T> CreateEnumerator(LinkedListNode<T> start, bool stop)
      {
         LinkedListNode<T> current = null;
         do
         {
            current = current == null ? start : current.Next ?? start;
            yield return current.Value;
         } while (!stop);
      }

      public void Stop()
      {
         _enumType.GetField("stop").SetValue(_enumerator, true);
      }
   }
}