using System;
using System.Collections.Generic;

namespace _10_FuncProgLambda
{
   /// <summary>
   /// Реализация рекурсивно-связного списка
   /// </summary>
   /// <typeparam name="T">Параметр типа</typeparam>
   public class MyList<T> : IMyList<T>
   {
      public static IMyList<T> CreateList(IEnumerable<T> items)
      {
         IEnumerator<T> iter = items.GetEnumerator();
         return CreateList(iter);
      }

      public static IMyList<T> CreateList(IEnumerator<T> iter)
      {
         Func<IMyList<T>> tailGenerator = null;
         tailGenerator = () => !iter.MoveNext()
            ? new MyList<T>(default(T), null)
            : new MyList<T>(iter.Current, tailGenerator);
         return tailGenerator();
      }

      public MyList(T head, Func<IMyList<T>> tailGenerator)
      {
         _head = head;
         _tailGenerator = tailGenerator;
      }

      public T Head { get { return _head; } }

      public IMyList<T> Tail
      {
         get
         {
            if (_tailGenerator == null)
               return null;
            return _tail ?? (_tail = _tailGenerator());
         }
      }

      private readonly T _head;
      private readonly Func<IMyList<T>> _tailGenerator;
      private IMyList<T> _tail;
   }
}
