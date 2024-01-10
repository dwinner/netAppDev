/**
 * Безопасная к модификации перечислителя через отражение реализация
 */

using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace _11_SecurityEnumerator
{
   internal class Program
   {
      private static void Main()
      {
         var ints = new EnumerableCollection<int>(new[] { 1, 2, 3, 4 });
         var enumerator = ints.GetEnumerator(true);
         // Получить ссылку на поле synchronized
         // Note: исключение object field = enumerator.GetType().GetField("synchronized").GetValue(enumerator);
      }

      /// <summary>
      ///    Оболочка для безопасности состояния перечислителя
      /// </summary>
      /// <typeparam name="T">Параметр типа</typeparam>
      public class EnumWrapper<T> : IEnumerator<T>
      {
         private readonly IEnumerator<T> _innerEnumerator;

         public EnumWrapper(IEnumerator<T> innerEnumerator) => _innerEnumerator = innerEnumerator;

         public void Dispose()
         {
            _innerEnumerator.Dispose();
         }

         public bool MoveNext() => _innerEnumerator.MoveNext();

         public void Reset()
         {
            _innerEnumerator.Reset();
         }

         public T Current => _innerEnumerator.Current;

         object IEnumerator.Current => Current;
      }

      /// <summary>
      ///    Перечислимый класс
      /// </summary>
      /// <typeparam name="T">Параметр типа</typeparam>
      public class EnumerableCollection<T> : IEnumerable<T>
      {
         private readonly T[] _items;

         public EnumerableCollection(T[] items) => _items = items;

         public IEnumerator<T> GetEnumerator() => GetEnumerator(false);

         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

         public IEnumerator<T> GetEnumerator(bool synchronized) =>
            new EnumWrapper<T>(GetPrivateEnumerator(synchronized));

         private IEnumerator<T> GetPrivateEnumerator(bool synchronized)
         {
            if (synchronized)
            {
               Monitor.Enter(_items.SyncRoot);
            }

            try
            {
               foreach (var item in _items)
               {
                  yield return item;
               }
            }
            finally
            {
               if (synchronized)
               {
                  Monitor.Exit(_items.SyncRoot);
               }
            }
         }
      }
   }
}