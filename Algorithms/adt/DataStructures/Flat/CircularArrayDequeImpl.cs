using System.Collections;

namespace DataStructures.Flat;

/// <summary>
///    Array based deque with circular behavior inside
/// </summary>
public sealed class CircularArrayDequeImpl<T> : IDeque<T>
{
   private const int DefaultSize = 4;
   private int _head; // the index of the first (oldest) item in the queue
   private T[] _items;
   private int _tail; // the index of the last (newest) item in the queue

   public CircularArrayDequeImpl()
   {
      _items = Array.Empty<T>();
      Count = 0;
      _head = 0;
      _tail = -1;
   }

   public int Count { get; private set; }

   public void EnqueueFirst(T item)
   {
      // if the array needs to grow
      if (_items.Length == Count)
      {
         AllocateNewArray(1);
      }

      // since we know the array isn't full and _head is greater than 0
      // we know the slot in front of head is open
      if (_head > 0)
      {
         _head--;
      }
      else
      {
         // otherwise we need to wrap around to the end of the array
         _head = _items.Length - 1;
      }

      _items[_head] = item;
      Count++;
   }

   public void EnqueueLast(T item)
   {
      // if the array needs to grow
      if (_items.Length == Count)
      {
         AllocateNewArray(0);
      }

      // now we have a properly sized array and can focus on wrapping issues.
      // if _tail is at the end of the array we need to wrap around
      if (_tail == _items.Length - 1)
      {
         _tail = 0;
      }
      else
      {
         _tail++;
      }

      _items[_tail] = item;
      Count++;
   }

   public T DequeueFirst()
   {
      if (Count == 0)
      {
         throw new InvalidOperationException("The deque is empty");
      }

      var value = _items[_head];
      if (_head == _items.Length - 1)
      {
         // if the head is at the last index in the array - wrap around.
         _head = 0;
      }
      else
      {
         // move to the next slot
         _head++;
      }

      Count--;

      return value;
   }

   public T DequeueLast()
   {
      if (Count == 0)
      {
         throw new InvalidOperationException("The deque is empty");
      }

      var value = _items[_tail];
      if (_tail == 0)
      {
         // if the tail is at the first index in the array - wrap around.
         _tail = _items.Length - 1;
      }
      else
      {
         // move to the previous slot
         _tail--;
      }

      Count--;

      return value;
   }

   public T PeekFirst()
   {
      if (Count == 0)
      {
         throw new InvalidOperationException("The deque is empty");
      }

      return _items[_head];
   }

   public T PeekLast()
   {
      if (Count == 0)
      {
         throw new InvalidOperationException("The deque is empty");
      }

      return _items[_tail];
   }

   public IEnumerator<T> GetEnumerator() =>
      _items.Where(item => item != null).GetEnumerator();

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private void AllocateNewArray(int startingIndex)
   {
      var newLen = Count == 0 ? DefaultSize : Count * 2;
      var newArray = new T[newLen];
      if (Count > 0)
      {
         var targetIndex = startingIndex;

         // copy contents...
         // if the array has no wrapping, just copy the valid range
         // else copy from head to end of the array and then from 0 to the tail

         // if tail is less than head we've wrapped
         if (_tail < _head)
         {
            // copy the _items[head].._items[end] -> newArray[0]..newArray[N]
            for (var index = _head; index < _items.Length; index++)
            {
               newArray[targetIndex] = _items[index];
               targetIndex++;
            }

            // copy _items[0].._items[tail] -> newArray[N+1]..
            for (var index = 0; index <= _tail; index++)
            {
               newArray[targetIndex] = _items[index];
               targetIndex++;
            }
         }
         else
         {
            // copy the _items[head].._items[tail] -> newArray[0]..newArray[N]
            for (var index = 0; index <= _tail; index++)
            {
               newArray[targetIndex] = _items[index];
               targetIndex++;
            }
         }

         _head = startingIndex;
         _tail = targetIndex - 1; // compensate for the extra bump
      }
      else
      {
         // nothing in the array
         _head = 0;
         _tail = -1;
      }

      _items = newArray;
   }
}