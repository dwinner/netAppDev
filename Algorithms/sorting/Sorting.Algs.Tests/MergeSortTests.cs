namespace Sorting.Algs.Tests;

public class MergeSortTests : SortTestsBase
{
   [Test]
   public void MergeSortTest()
   {
      MergeSort.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }
}