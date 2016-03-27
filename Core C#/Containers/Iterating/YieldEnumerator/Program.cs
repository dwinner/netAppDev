/**
 * Автоматическая генерация перечислителей на базе yield-блоков кода
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _10_YieldEnumerator
{
   class Program
   {
      static void Main()
      {
         var myColl = new MyColl<int>(new[] { 1, 2, 3, 4 });
         foreach (int i in myColl)
         {
            Console.WriteLine(i);
         }

         var collExtension = new MyCollExtension<int>(myColl);
         IEnumerator<int> enumerator = collExtension.GetEnumerator(false);
         while (enumerator.MoveNext())
         {
            Console.WriteLine(enumerator.Current);
         }

         Console.ReadKey();
      }

      public class MyColl<T> : IEnumerable<T>
      {
         private readonly T[] _items;

         public T[] Items { get { return _items; } }

         public MyColl(T[] items)
         {
            _items = items;
         }

         protected MyColl()
         {
            // throw new NotSupportedException("Default ctor not supported");
         }

         public IEnumerator<T> GetEnumerator()
         {
            // Note. Можно и так: return ((IEnumerable<T>) _items).GetEnumerator();
            // Note. Или так:
            //foreach (T item in _items)
            //{
            //   yield return item;
            //}
            lock (_items.SyncRoot)
            {
               for (int i = 0; i < _items.Length; i++)
               {
                  yield return _items[i];
               }
            }
         }

         IEnumerator IEnumerable.GetEnumerator()
         {
            return GetEnumerator();
         }
      }

      public class MyCollExtension<T> : MyColl<T>
      {
         private readonly MyColl<T> _myColl;

         public MyCollExtension(T[] items)
            : base(items)
         {
            _myColl = new MyColl<T>(items);
         }

         public MyCollExtension(MyColl<T> myColl)
         {
            _myColl = myColl;
         }

         public IEnumerator<T> GetEnumerator(bool synchronized)
         {
            if (synchronized)
            {
               Monitor.Enter(_myColl.Items.SyncRoot);
            }
            try
            {
               int index = 0;
               while (true)
               {
                  if (index < _myColl.Items.Length)
                     yield return _myColl.Items[index++];
                  else
                     yield break;
               }
            }
            finally
            {
               if (synchronized)
               {
                  Monitor.Exit(_myColl.Items.SyncRoot);
               }
            }
         }
      }
   }
}
