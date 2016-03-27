using System.Collections.Generic;

namespace SimpleDataStructures
{
   public interface IQueue<T> : IEnumerable<T>
   {
      void Enqueue(T item);
      T Deque();
      int Size { get; }
      bool IsEmpty { get; }      
   }
}