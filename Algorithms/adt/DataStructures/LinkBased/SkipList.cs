using System.Collections;

namespace DataStructures.LinkBased;

public class SkipList<T> : ICollection<T>
   where T : IComparable<T>
{
   private const int DefaultHeight = 0x21;

   // used to determine the random height of the node links
   private readonly Random _rand = new();

   // the number of items currently in the list

   // the non-data node which starts the list
   private SkipListNode<T> _head = null!;

   // there is always one level of depth (the base list)
   private int _levels;

   public SkipList() => Clear();

   public void Add(T item)
   {
      var level = PickRandomLevel();
      var newNode = new SkipListNode<T>(item, level + 1);
      var currentNode = _head;
      for (var i = _levels - 1; i >= 0; i--)
      {
         while (currentNode?.Next[i] != null)
         {
            if (currentNode.Next[i]?.Value.CompareTo(item) > 0)
            {
               break;
            }

            currentNode = currentNode.Next[i];
         }

         if (i <= level)
         {
            // adding "c" to the list: a -> b -> d -> e
            // current is node b and current.Next[i] is d.

            // 1. Link the new node (c) to the existing node (d)
            // c.Next = d
            newNode.Next[i] = currentNode?.Next[i];

            // Insert c into the list after b
            // b.Next = c
            currentNode!.Next[i] = newNode;
         }
      }

      Count++;
   }

   public bool Contains(T item)
   {
      var currentNode = _head;
      for (var i = _levels - 1; i >= 0; i--)
      {
         while (currentNode?.Next[i] != null)
         {
            var cmpRes = currentNode.Next[i]?.Value.CompareTo(item);
            if (cmpRes > 0)
            {
               // the value is too large so go down one level
               // and take smaller steps
               break;
            }

            if (cmpRes == 0)
            {
               // found it!
               return true;
            }

            currentNode = currentNode.Next[i];
         }
      }

      return false;
   }

   public bool Remove(T item)
   {
      var currentNode = _head;
      var removed = false;
      // walk down each level in the list (make big jumps)
      for (var level = _levels - 1; level >= 0; level--)
      {
         // while we're not at the end of the list
         while (currentNode?.Next[level] != null)
         {
            // if we found our node
            if (currentNode.Next[level]?.Value.CompareTo(item) == 0)
            {
               // remove the node
               currentNode.Next[level] = currentNode.Next[level]?.Next[level];
               removed = true;

               // and go down to the next level (where
               // we will find our node again if we're
               // not at the bottom level)
               break;
            }

            // if we went too far then go down a level
            if (currentNode.Next[level]?.Value.CompareTo(item) > 0)
            {
               break;
            }

            currentNode = currentNode.Next[level];
         }
      }

      if (removed)
      {
         Count--;
      }

      return removed;
   }

   public void Clear()
   {
      _head = new SkipListNode<T>(default!, DefaultHeight);
      Count = 0;
      _levels = 1;
   }

   public void CopyTo(T[] array, int arrayIndex)
   {
      if (array == null)
      {
         throw new ArgumentNullException(nameof(array));
      }

      var offset = 0;
      foreach (var item in this)
      {
         array[arrayIndex + offset++] = item;
      }
   }

   public int Count { get; private set; }

   public bool IsReadOnly => false;

   public IEnumerator<T> GetEnumerator()
   {
      var cur = _head.Next[0];
      while (cur != null)
      {
         yield return cur.Value;
         cur = cur.Next[0];
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   private int PickRandomLevel()
   {
      var rand = _rand.Next();
      var level = 0;

      // we're using the bit mask of a random integer to determine if the max
      // level should bump up one or not.
      // Say the 8 LSB of the int are 00101100. In that case when the
      // LSB is compared against 1 it tests to 0 and the while loop is never
      // entered so the level stays the same. That should happen 1/2 of the time.
      // Later if the _levels field is set to 3 and the rand value is 01101111,
      // this time the while loop will run 4 times and on the last iteration will
      // run 4 times creating a node with a skip list height of 4. This should
      // only happen 1/16 of the time.
      while ((rand & 1) == 1)
      {
         if (level == _levels)
         {
            _levels++;
            break;
         }

         rand >>= 1;
         level++;
      }

      return level;
   }

   internal class SkipListNode<TItem>
   {
      /// <summary>
      ///    Creates a new node with the specified value
      ///    and a the indicated link height.
      /// </summary>
      public SkipListNode(TItem value, int height)
      {
         Value = value;
         Next = new SkipListNode<TItem>[height];
      }

      /// <summary>
      ///    The array of links. The number of items
      ///    is the height of the links.
      /// </summary>
      public SkipListNode<TItem>?[] Next { get; }

      /// <summary>
      ///    The contained value.
      /// </summary>
      public TItem Value { get; }
   }
}