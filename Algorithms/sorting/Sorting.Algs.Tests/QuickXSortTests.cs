namespace Sorting.Algs.Tests;

public class QuickXSortTests : SortTestsBase
{
   [Test]
   public void QuickXSortTest()
   {
      QuickXSort.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }
}