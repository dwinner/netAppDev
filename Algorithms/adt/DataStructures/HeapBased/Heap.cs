namespace DataStructures.HeapBased;

public sealed class Heap<T> where T : IComparable<T>
{
   private const int DefaultLength = 100;
   private T[] _items;

   public Heap()
      : this(DefaultLength)
   {
   }

   public Heap(int length)
   {
      _items = new T[length];
      Count = 0;
   }

   public int Count { get; private set; }

   public void Add(T value)
   {
      if (Count >= _items.Length)
      {
         GrowBackingArray();
      }

      _items[Count] = value;
      var index = Count;
      while (index > 0 && _items[index].CompareTo(_items[GetParent(index)]) > 0)
      {
         Swap(index, GetParent(index));
         index = GetParent(index);
      }

      Count++;
   }

   private void GrowBackingArray() =>
      Array.Resize(ref _items, _items.Length * 2);

   public T Peek()
   {
      if (Count > 0)
      {
         return _items[0];
      }

      throw new InvalidOperationException();
   }

   public T RemoveMax()
   {
      if (Count <= 0)
      {
         throw new InvalidOperationException();
      }

      var max = _items[0];
      _items[0] = _items[Count - 1];
      Count--;
      var index = 0;
      while (index < Count)
      {
         // get the left and right child indexes
         var left = 2 * index + 1;
         var right = 2 * index + 2;
         // make sure we are still within the heap
         if (left >= Count)
         {
            break;
         }

         // To avoid having to swap twice, we swap with the largest value.
         // E.g.,
         //      5
         //    6   8
         // 
         // if we swapped with 6 first we'd have
         //
         //      6
         //   5    8
         //
         // And we'd require another swap to get the desired tree
         //
         //     8
         //  6    5
         //
         // So we find the largest child and just do the right thing off the bat
         var maxChildIndex = GetIndexOfMaxChild(left, right);
         if (_items[index].CompareTo(_items[maxChildIndex]) > 0)
         {
            // the current item is larger than its children (heap property is satisfied)
            break;
         }

         Swap(index, maxChildIndex);
         index = maxChildIndex;
      }

      return max;
   }

   private int GetIndexOfMaxChild(int left, int right)
   {
      // Find the index of the child with the largest value
      var maxChildIndex =
         right >= Count
            ? left // No right child
            : _items[left].CompareTo(_items[right]) > 0
               ? left
               : right;

      return maxChildIndex;
   }

   public void Clear()
   {
      Count = 0;
      _items = new T[DefaultLength];
   }

   private static int GetParent(int index) => (index - 1) / 2;

   private void Swap(int left, int right) =>
      (_items[left], _items[right]) = (_items[right], _items[left]);
}