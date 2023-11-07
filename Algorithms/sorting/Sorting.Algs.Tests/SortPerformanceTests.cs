using System.Collections;

namespace Sorting.Algs.Tests;

public class SortPerformanceTests : SortTestsBase
{
   [TestCaseSource(typeof(SortPerformanceTests), nameof(SortCases))]
   public void SortPerformanceTest(string sortName, Action sortAction) => sortAction();

   public static IEnumerable SortCases
   {
      get
      {
         yield return new TestCaseData("QuickX", () => QuickXSort.Sort(ArrayToSort))
            .SetDescription("QuickX");
         yield return new TestCaseData("Shell", () => Shell.Sort(ArrayToSort))
            .SetDescription("Shell");
         yield return new TestCaseData("Selection", () => SelectionSort.Sort(ArrayToSort))
            .SetDescription("Selection");
         yield return new TestCaseData("Quick", () => Quick.Sort(ArrayToSort))
            .SetDescription("Quick");
         yield return new TestCaseData("Quick3Way", () => Quick3Way.Sort(ArrayToSort))
            .SetDescription("Quick3Way");
         yield return new TestCaseData("Merge", () => MergeSort.Sort(ArrayToSort))
            .SetDescription("Merge");
         yield return new TestCaseData("Insertion", () => InsertionSort.Sort(ArrayToSort))
            .SetDescription("Insertion");
         yield return new TestCaseData("System", () => Array.Sort(ArrayToSort))
            .SetDescription("System");
      }
   }
}