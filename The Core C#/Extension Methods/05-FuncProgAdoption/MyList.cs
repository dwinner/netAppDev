using System.Collections.Generic;

namespace _05_FuncProgAdoption
{
   public class MyList<T> : IRecursiveList<T>
   {
      private readonly T _head;
      public T Head { get { return _head; } }

      private readonly IRecursiveList<T> _tail;
      public IRecursiveList<T> Tail { get { return _tail; } }

      public static IRecursiveList<T> CreateList(IEnumerable<T> items)
      {
         IEnumerator<T> iter = items.GetEnumerator();
         return CreateList(iter);
      }

      public static IRecursiveList<T> CreateList(IEnumerator<T> iter)
      {
         return !iter.MoveNext()
            ? new MyList<T>(default(T), null)
            : new MyList<T>(iter.Current, CreateList(iter));
      }

      public MyList(T head, IRecursiveList<T> tail)
      {
         _head = head;
         _tail = tail;
      }
   }
}