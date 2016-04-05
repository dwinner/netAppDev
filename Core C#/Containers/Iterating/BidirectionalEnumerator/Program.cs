/** 
 * Двунаправленный итератор
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace _14_BidirectionalEnumerator
{
   class Program
   {
      static void Main()
      {
         var intLinkedList = new LinkedList<int>();
         for (int i = 1; i < 6; ++i)
         {
            intLinkedList.AddLast(i);
         }
         var iterator = new BidirectionalIterator<int>(intLinkedList.First, IteratorDirection.Forward);

         foreach (int n in iterator)
         {
            Console.WriteLine(n);
            if (n == 5)
               iterator.Direction = IteratorDirection.Backward;
         }

         Console.ReadKey();
      }
   }

   /// <summary>
   /// Направление итерации
   /// </summary>
   public enum IteratorDirection
   {
      Forward,
      Backward
   }

   /// <summary>
   /// Логика двунаправленного итератора
   /// </summary>
   /// <typeparam name="T">Параметр типа для двусвязного списка</typeparam>
   public class BidirectionalIterator<T> : IEnumerable<T>
   {
      private readonly IEnumerator<T> _enumerator;
      private readonly Type _enumType;

      public BidirectionalIterator(LinkedListNode<T> start, IteratorDirection direction)
      {
         _enumerator = CreateEnumerator(start, direction).GetEnumerator();
         _enumType = _enumerator.GetType();
      }

      public IteratorDirection Direction
      {
         get { return (IteratorDirection)_enumType.GetField("direction").GetValue(_enumerator); }
         set { _enumType.GetField("direction").SetValue(_enumerator, value); }
      }

      private IEnumerable<T> CreateEnumerator(LinkedListNode<T> start, IteratorDirection direction)
      {
         // Note: start и direction запоминаются в виде открытых полей итератора,
         // Note: созданного в yield-блоке
         LinkedListNode<T> current = null;
         do
         {
            if (current == null)
               current = start;
            else
               current = direction == IteratorDirection.Forward ? current.Next : current.Previous;
            if (current != null)
               yield return current.Value;
         }
         while (current != null);
      }

      public IEnumerator<T> GetEnumerator()
      {
         return _enumerator;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }
   }
}
