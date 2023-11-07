namespace DataStructures;

public sealed class SortingSupport<T> where T : IComparable<T>
{
   private static void Swap(IList<T> items, int left, int right)
   {
      if (left == right)
      {
         return;
      }

      (items[left], items[right]) = (items[right], items[left]);
   }

   #region Bubble Sort

   public void BubbleSort(T[] items)
   {
      bool swapped;

      do
      {
         swapped = false;
         for (var i = 1; i < items.Length; i++)
         {
            if (items[i - 1].CompareTo(items[i]) > 0)
            {
               Swap(items, i - 1, i);
               swapped = true;
            }
         }
      } while (swapped);
   }

   #endregion

   #region Insertion Sort

   public void InsertionSort(T[] items)
   {
      var sortedRangeEndIndex = 1;
      while (sortedRangeEndIndex < items.Length)
      {
         if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
         {
            var insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
            Insert(items, insertIndex, sortedRangeEndIndex);
         }

         sortedRangeEndIndex++;
      }
   }

   private static int FindInsertionIndex(IReadOnlyList<T> items, T valueToInsert)
   {
      for (var index = 0; index < items.Count; index++)
      {
         if (items[index].CompareTo(valueToInsert) > 0)
         {
            return index;
         }
      }

      throw new InvalidOperationException("The insertion index was not found");
   }

   private static void Insert(IList<T> itemArray, int indexInsertingAt, int indexInsertingFrom)
   {
      // itemArray =       0 1 2 4 5 6 3 7
      // insertingAt =     3
      // insertingFrom =   6
      // actions
      //  1: store index at in temp     temp = 4
      //  2: set index at to index from  -> 0 1 2 3 5 6 3 7   temp = 4
      //  3: walking backwards from index from to index at + 1
      //     shift values from left to right once
      //     0 1 2 3 5 6 6 7   temp = 4
      //     0 1 2 3 5 5 6 7   temp = 4
      //  4: write temp value to index at + 1
      //     0 1 2 3 4 5 6 7   temp = 4

      // Step 1
      var temp = itemArray[indexInsertingAt];

      // Step 2

      itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];

      // Step 3
      for (var current = indexInsertingFrom; current > indexInsertingAt; current--)
      {
         itemArray[current] = itemArray[current - 1];
      }

      // Step 4
      itemArray[indexInsertingAt + 1] = temp;
   }

   #endregion

   #region Selection Sort

   public void SelectionSort(T[] items)
   {
      var sortedRangeEnd = 0;

      while (sortedRangeEnd < items.Length)
      {
         var nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
         Swap(items, sortedRangeEnd, nextIndex);

         sortedRangeEnd++;
      }
   }

   private static int FindIndexOfSmallestFromIndex(IReadOnlyList<T> items, int sortedRangeEnd)
   {
      var currentSmallest = items[sortedRangeEnd];
      var currentSmallestIndex = sortedRangeEnd;

      for (var i = sortedRangeEnd + 1; i < items.Count; i++)
      {
         if (currentSmallest.CompareTo(items[i]) > 0)
         {
            currentSmallest = items[i];
            currentSmallestIndex = i;
         }
      }

      return currentSmallestIndex;
   }

   #endregion

   #region Merge Sort

   public void MergeSort(T[] items)
   {
      if (items.Length <= 1)
      {
         return;
      }

      var leftSize = items.Length / 2;
      var rightSize = items.Length - leftSize;

      var left = new T[leftSize];
      var right = new T[rightSize];

      Array.Copy(items, 0, left, 0, leftSize);
      Array.Copy(items, leftSize, right, 0, rightSize);

      MergeSort(left);
      MergeSort(right);
      Merge(items, left, right);
   }

   private static void Merge(IList<T> items, IReadOnlyList<T> left, IReadOnlyList<T> right)
   {
      var leftIndex = 0;
      var rightIndex = 0;
      var targetIndex = 0;
      var remaining = left.Count + right.Count;
      while (remaining > 0)
      {
         if (leftIndex >= left.Count)
         {
            items[targetIndex] = right[rightIndex++];
         }
         else if (rightIndex >= right.Count)
         {
            items[targetIndex] = left[leftIndex++];
         }
         else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
         {
            items[targetIndex] = left[leftIndex++];
         }
         else
         {
            items[targetIndex] = right[rightIndex++];
         }

         targetIndex++;
         remaining--;
      }
   }

   #endregion

   #region Quick Sort

   private readonly Random _pivotRng = new();

   public void QuickSort(T[] items)
   {
      Quicksort(items, 0, items.Length - 1);
   }

   private void Quicksort(IList<T> items, int left, int right)
   {
      if (left < right)
      {
         var pivotIndex = _pivotRng.Next(left, right);
         var newPivot = Partition(items, left, right, pivotIndex);

         Quicksort(items, left, newPivot - 1);
         Quicksort(items, newPivot + 1, right);
      }
   }

   private static int Partition(IList<T> items, int left, int right, int pivotIndex)
   {
      var pivotValue = items[pivotIndex];

      Swap(items, pivotIndex, right);

      var storeIndex = left;

      for (var i = left; i < right; i++)
      {
         if (items[i].CompareTo(pivotValue) < 0)
         {
            Swap(items, i, storeIndex);
            storeIndex += 1;
         }
      }

      Swap(items, storeIndex, right);
      return storeIndex;
   }

   #endregion
}