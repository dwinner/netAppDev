/**
 * Ручная реализация перечислителя для коллекций
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _09_ByHandEnumerator
{
   class Program
   {
      static void Main()
      {
         var ints = new MyColl<int>(new[] { 1, 2, 3, 4 });
         foreach (int n in ints)
         {
            Console.WriteLine(n);
         }

         Console.ReadKey();
      }
   }

   public class MyColl<T> : IEnumerable<T>
   {
      private readonly T[] _items;

      public MyColl(T[] items)
      {
         _items = items;
      }

      public IEnumerator<T> GetEnumerator()
      {
         return new NestedEnumerator(this);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      private class NestedEnumerator : IEnumerator<T>
      {
         private readonly MyColl<T> _coll;
         private int _index;

         public NestedEnumerator(MyColl<T> coll)
         {
            _coll = coll;
            Monitor.Enter(_coll._items.SyncRoot);
            _index = -1;
         }

         public void Dispose()
         {
            try
            {
               Current = default(T);
               _index = 0;
            }
            finally
            {
               Monitor.Exit(_coll._items.SyncRoot);
            }
         }

         public bool MoveNext()
         {
            if (++_index >= _coll._items.Length)
               return false;
            Current = _coll._items[_index];
            return true;
         }

         public void Reset()
         {
            Current = default(T);
            _index = 0;
         }

         public T Current { get; private set; }

         object IEnumerator.Current
         {
            get { return Current; }
         }
      }
   }
}
