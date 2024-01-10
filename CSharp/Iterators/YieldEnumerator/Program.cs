/**
 * Автоматическая генерация перечислителей на базе yield-блоков кода
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _10_YieldEnumerator
{
   internal class Program
   {
      private static void Main()
      {
         var myColl = new MyColl<int>(new[] { 1, 2, 3, 4 });
         foreach (var i in myColl)
         {
            Console.WriteLine(i);
         }

         var collExtension = new MyCollExtension<int>(myColl);
         var enumerator = collExtension.GetEnumerator(false);
         while (enumerator.MoveNext())
         {
            Console.WriteLine(enumerator.Current);
         }

         Console.ReadKey();
      }

      public class MyColl<T> : IEnumerable<T>
      {
         public MyColl(T[] items) => Items = items;

         protected MyColl()
         {
            // throw new NotSupportedException("Default ctor not supported");
         }

         public T[] Items { get; }

         public IEnumerator<T> GetEnumerator()
         {
            // Note. Можно и так: return ((IEnumerable<T>) _items).GetEnumerator();
            // Note. Или так:
            //foreach (T item in _items)
            //{
            //   yield return item;
            //}
            lock (Items.SyncRoot)
            {
               for (var i = 0; i < Items.Length; i++)
               {
                  yield return Items[i];
               }
            }
         }

         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
      }

      public class MyCollExtension<T> : MyColl<T>
      {
         private readonly MyColl<T> _myColl;

         public MyCollExtension(T[] items)
            : base(items) =>
            _myColl = new MyColl<T>(items);

         public MyCollExtension(MyColl<T> myColl) => _myColl = myColl;

         public IEnumerator<T> GetEnumerator(bool synchronized)
         {
            if (synchronized)
            {
               Monitor.Enter(_myColl.Items.SyncRoot);
            }

            try
            {
               var index = 0;
               while (true)
               {
                  if (index < _myColl.Items.Length)
                  {
                     yield return _myColl.Items[index++];
                  }
                  else
                  {
                     yield break;
                  }
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