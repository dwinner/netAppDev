using MoreLinq;

namespace Sorting.Algs.Tests;

public class SortTestsBase
{
   [SetUp]
   public void SetUp()
   {
      Array.Copy(InitialArray, ArrayToSort, InitialArray.Length);
   }

   [TearDown]
   public void Reset()
   {
      Array.Clear(ArrayToSort, 0, ArrayToSort.Length);
   }

   static SortTestsBase()
   {
      InitialArray = Enumerable.Range(0, ArrayLength).Shuffle().ToArray();
      ArrayToSort = new int[ArrayLength];
   }

   private const int ArrayLength = 100_000;

   private static readonly int[] InitialArray;
   protected static readonly int[] ArrayToSort;
}