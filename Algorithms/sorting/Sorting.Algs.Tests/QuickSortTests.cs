namespace Sorting.Algs.Tests;

public class QuickSortTests : SortTestsBase
{
   [Test]
   public void QuickSortTest()
   {
      Quick.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }
}