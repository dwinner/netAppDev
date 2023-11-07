namespace Sorting.Algs.Tests;

public class Quick3WaySortTests : SortTestsBase
{
   [Test]
   public void Quick3WaySortTest()
   {
      Quick3Way.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }
}