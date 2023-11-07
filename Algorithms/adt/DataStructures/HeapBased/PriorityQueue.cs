namespace DataStructures.HeapBased;

public sealed class PriorityQueue<T> where T : IComparable<T>
{
   private readonly Heap<T> _heap = new();

   public int Count => _heap.Count;

   public void Enqueue(T value) => _heap.Add(value);

   public T Dequeue() => _heap.RemoveMax();

   public void Clear() => _heap.Clear();
}