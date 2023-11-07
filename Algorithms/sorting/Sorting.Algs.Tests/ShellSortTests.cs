namespace Sorting.Algs.Tests;

public class ShellSortTests : SortTestsBase
{
   [Test]
   public void ShellSortTest()
   {
      Shell.Sort(ArrayToSort);
      var isSorted = ArrayToSort.IsSorted();
      Assert.That(isSorted, Is.True);
   }
}