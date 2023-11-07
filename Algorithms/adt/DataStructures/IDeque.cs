namespace DataStructures;

/// <summary>
///    Behavioral interface for Deque data structure
/// </summary>
/// <typeparam name="T">Element type</typeparam>
public interface IDeque<T> : IEnumerable<T>
{
   /// <summary>
   ///    Element count
   /// </summary>
   int Count { get; }

   /// <summary>
   ///    Enqueue to the head
   /// </summary>
   /// <param name="item">Item to enqueue</param>
   void EnqueueFirst(T item);

   /// <summary>
   ///    Enqueue to the tail
   /// </summary>
   /// <param name="item">Item to enqueue</param>
   void EnqueueLast(T item);

   /// <summary>
   ///    Dequeue from the head
   /// </summary>
   /// <returns>Item to dequeue</returns>
   T DequeueFirst();

   /// <summary>
   ///    Dequeue from the tail
   /// </summary>
   /// <returns>Item to dequeue</returns>
   T DequeueLast();

   /// <summary>
   ///    Peek from the head
   /// </summary>
   /// <returns>Item to peek</returns>
   T PeekFirst();

   /// <summary>
   ///    Peek from the tail
   /// </summary>
   /// <returns>Item to peek</returns>
   T PeekLast();
}