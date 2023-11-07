namespace Sorting.Algs.Tests;

public class InsertionSortTests : SortTestsBase
{
   [Test]
   public void InsertionSortTest()
   {
      InsertionSort.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }

   [Test]
   public void InsertionSortUsingComparerTest()
   {
      var defaultComparer = Comparer<int>.Default;
      InsertionSort.Sort(ArrayToSort, defaultComparer);
      var isSorted = ArrayToSort.IsSorted(defaultComparer);
      Assert.That(isSorted, Is.True);
   }
}