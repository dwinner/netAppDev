namespace DataStructures.LinkBased;

public sealed class Queue2<T>
{
   private readonly LinkBased.LinkedList<T> _items = new();

   public int Count => _items.Count;

   public void Enqueue(T value)
   {
      _items.AddFirst(value);
   }

   public T? Dequeue()
   {
      if (_items.Count == 0)
      {
         throw new InvalidOperationException("The queue is empty");
      }

      return _items.RemoveLast();
   }

   public T? Peek()
   {
      if (_items.Count == 0)
      {
         throw new InvalidOperationException("The queue is empty");
      }

      return _items.Tail != null ? _items.Tail.Value : default;
   }
}