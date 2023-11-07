namespace DataStructures.LinkBased;

public sealed class Stack2<T>
{
   private readonly LinkBased.LinkedList<T> _items = new();

   public int Count => _items.Count;

   public void Push(T value)
   {
      _items.AddLast(value);
   }

   public T? Pop()
   {
      if (_items.Count == 0)
      {
         throw new InvalidOperationException("The stack is empty");
      }

      return _items.RemoveLast();
   }

   public T? Peek()
   {
      if (_items.Count == 0)
      {
         throw new InvalidOperationException("The stack is empty");
      }

      return _items.Tail != null ? _items.Tail.Value : default;
   }
}