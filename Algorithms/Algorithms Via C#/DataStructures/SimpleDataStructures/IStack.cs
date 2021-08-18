using System.Collections.Generic;

namespace SimpleDataStructures
{
   public interface IStack<T> : IEnumerable<T>
   {
      bool IsEmpty { get; }
      int Size { get; }
      void Push(T anItem);
      T Pop();
   }
}