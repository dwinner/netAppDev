using System.Collections;

namespace DataStructures.LinkBased;

/// <summary>
///    Linked list
/// </summary>
/// <typeparam name="T">Item type</typeparam>
public sealed class LinkedList<T> : ICollection<T>
{
   public LinkedListNode<T>? Tail { get; private set; }

   public LinkedListNode<T>? Head { get; private set; }

   public IEnumerator<T> GetEnumerator()
   {
      var currentNode = Head;
      while (currentNode != null)
      {
         yield return currentNode.Value!;
         currentNode = currentNode.Next;
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public void Add(T item) => AddLast(item);

   public void Clear()
   {
      Head = null;
      Tail = null;
      Count = 0;
   }

   public bool Contains(T item)
   {
      var currentNode = Head;
      while (currentNode != null)
      {
         if (currentNode.Value != null && currentNode.Value.Equals(item))
         {
            return true;
         }

         currentNode = currentNode.Next;
      }

      return false;
   }

   public void CopyTo(T[] array, int arrayIndex)
   {
      var currentNode = Head;
      while (currentNode != null)
      {
         array[arrayIndex++] = currentNode.Value!;
         currentNode = currentNode.Next;
      }
   }

   public bool Remove(T item)
   {
      LinkedListNode<T>? previous = null;
      var current = Head;

      // 1: Empty list - do nothing
      // 2: Single node: (previous is null)
      // 3: Many nodes
      //    a: node to remove is the first node
      //    b: node to remove is the middle or last

      while (current != null)
      {
         // Head -> 3 -> 5 -> 7 -> null
         // Head -> 3 ------> 7 -> null
         if (current.Value != null && current.Value.Equals(item))
         {
            // it's a node in the middle or end
            if (previous != null)
            {
               // Case 3b
               previous.Next = current.Next;

               // it was the end - so update Tail
               if (current.Next == null)
               {
                  Tail = previous;
               }
               else
               {
                  // Before: Head -> 3 <-> 5 <-> 7 -> null
                  // After:  Head -> 3 <-------> 7 -> null

                  // previous = 3
                  // current = 5
                  // current.Next = 7
                  // So... 7.Previous = 3
                  current.Next.Previous = previous;
               }

               Count--;
            }
            else
            {
               // Case 2 or 3a
               RemoveFirst();
            }

            return true;
         }

         previous = current;
         current = current.Next;
      }

      return false;
   }

   public int Count { get; private set; }

   public bool IsReadOnly => false;

   public void AddFirst(T value)
   {
      var newNode = new LinkedListNode<T>(value);

      // Save off the head node so we don't lose it
      var oldHead = Head;

      // Point head to the new node
      Head = newNode;

      // Insert the rest of the list behind head
      Head.Next = oldHead;

      if (Count == 0)
      {
         // if the list was empty then head  and tail should
         // both point to the new node.
         Tail = Head;
      }
      else
      {
         // Before: head -------> 5 <-> 7 -> null
         // After:  head  -> 3 <-> 5 <-> 7 -> null
         oldHead!.Previous = Head;
      }

      Count++;
   }

   public void AddLast(T value)
   {
      var newNode = new LinkedListNode<T>(value);

      if (Count == 0)
      {
         Head = newNode;
      }
      else
      {
         Tail!.Next = newNode;

         // Before: Head -> 3 <-> 5 -> null
         // After:  Head -> 3 <-> 5 <-> 7 -> null
         // 7.Previous = 5
         newNode.Previous = Tail;
      }

      Tail = newNode;
      Count++;
   }

   public T? RemoveFirst()
   {
      if (Count == 0)
      {
         return default;
      }

      var removedValue = Head!.Value;

      // Before: Head -> 3 <-> 5
      // After:  Head -------> 5
      // Head -> 3 -> null
      // Head ------> null
      Head = Head.Next;
      Count--;
      if (Count == 0)
      {
         Tail = null;
      }
      else
      {
         // 5.Previous was 3, now null
         Head!.Previous = null;
      }

      return removedValue;
   }

   public T? RemoveLast()
   {
      if (Count == 0)
      {
         return default;
      }

      T? removedValue;
      if (Count == 1)
      {
         removedValue = Head!.Value;
         Head = null;
         Tail = null;
      }
      else
      {
         removedValue = Tail!.Value;
         // Before: Head --> 3 --> 5 --> 7
         //         Tail = 7
         // After:  Head --> 3 --> 5 --> null
         //         Tail = 5
         // Null out 5's Next pointerproperty
         Tail.Previous!.Next = null;
         Tail = Tail.Previous;
      }

      Count--;

      return removedValue;
   }
}