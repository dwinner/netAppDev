using System.Collections;

namespace DataStructures.Flat;

/// <summary>
///    Array list impl. with Grow strategy
/// </summary>
/// <typeparam name="T">Item type</typeparam>
public sealed class ArrayList<T> : IList<T>
{
   private T[] _items;

   public ArrayList(int length)
   {
      if (length < 0)
      {
         throw new ArgumentException($"{length:D} cannot be negative", nameof(length));
      }

      _items = new T[length];
   }

   public ArrayList() : this(0)
   {
   }

   public IEnumerator<T> GetEnumerator()
   {
      for (var i = 0; i < Count; i++)
      {
         yield return _items[i];
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

   public void Add(T item)
   {
      if (_items.Length == Count)
      {
         GrowArray();
      }

      _items[Count++] = item;
   }

   public void Clear()
   {
      _items = Array.Empty<T>();
      Count = 0;
   }

   public bool Contains(T item) => IndexOf(item) > -1;

   public void CopyTo(T[] array, int arrayIndex) =>
      Array.Copy(_items, 0, array, arrayIndex, Count);

   public bool Remove(T item)
   {
      for (var i = 0; i < Count; i++)
      {
         if (_items[i]!.Equals(item))
         {
            RemoveAt(i);
            return true;
         }
      }

      return false;
   }

   public int Count { get; private set; }

   public bool IsReadOnly => false;

   public int IndexOf(T item)
   {
      for (var i = 0; i < Count; i++)
      {
         if (_items[i]!.Equals(item))
         {
            return i;
         }
      }

      return -1;
   }

   public void Insert(int index, T item)
   {
      if (index < 0 || index >= Count)
      {
         throw new IndexOutOfRangeException($"index {index} is out of range");
      }

      if (_items.Length == Count)
      {
         GrowArray();
      }

      // shift all the items following index one to the right
      Array.Copy(_items, index, _items, index + 1, Count - index);
      _items[index] = item;
      Count++;
   }

   public void RemoveAt(int index)
   {
      if (index < 0 || index >= Count)
      {
         throw new IndexOutOfRangeException($"index {index} is out of range");
      }

      var shiftStart = index + 1;
      if (shiftStart < Count)
      {
         // shift all the items following index one to the left
         Array.Copy(_items, shiftStart, _items, index, Count - shiftStart);
      }

      Count--;
   }

   public T this[int index]
   {
      get
      {
         if (index < 0 || index >= Count)
         {
            throw new IndexOutOfRangeException($"index {index} is out of range");
         }

         return _items[index];
      }
      set
      {
         if (index < 0 || index >= Count)
         {
            throw new IndexOutOfRangeException($"index {index} is out of range");
         }

         _items[index] = value;
      }
   }

   private void GrowArray()
   {
      var newLen = _items.Length == 0 ? 16 : _items.Length << 1;
      var newArray = new T[newLen];
      _items.CopyTo(newArray, 0);
      _items = newArray;
   }
}