namespace Sorting.Algs.Tests;

public class SelectionSortTests : SortTestsBase
{
   [Test]
   public void SimpleSelectionSortTest()
   {
      SelectionSort.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }

   [Test]
   public void SelectionSortUsingComparerTest()
   {
      var defaultComparer = Comparer<int>.Default;
      SelectionSort.Sort(ArrayToSort, defaultComparer);
      var isSorted = ArrayToSort.IsSorted(defaultComparer);
      Assert.That(isSorted, Is.True);
   }

   [Test]
   public void SelectionSortUsingFuncTest()
   {
      SelectionSort.Sort(ArrayToSort, CompFunc);
      var isSorted = ArrayToSort.IsSorted(CompFunc);
      Assert.That(isSorted, Is.True);

      static int CompFunc(int item1, int item2) => item1 > item2
         ? 1
         : item1 == item2
            ? 0
            : -1;
   }
}