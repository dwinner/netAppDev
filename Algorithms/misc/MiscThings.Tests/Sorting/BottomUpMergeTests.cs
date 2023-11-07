using static MiscThings.Sorting.BottomUpMerge;

namespace MiscThings.Tests.Sorting;

using static SortSources;

[TestFixture]
public class BottomUpMergeTests
{
   [SetUp]
   public void SetUp()
   {
      _array1 = new string[Tiny.Length];
      for (var i = 0; i < Tiny.Length; i++)
      {
         _array1[i] = Tiny[i];
      }

      _array2 = new string[Words.Length];
      for (var i = 0; i < Words.Length; i++)
      {
         _array2[i] = Words[i];
      }
   }

   private string[] _array1 = null!;
   private string[] _array2 = null!;

   [Test]
   public void BottomUpMergeTest()
   {
      Sort(_array1);
      Array.ForEach(_array1, item => Console.Write($"{item} "));
      Console.WriteLine();
      Sort(_array2);
      Array.ForEach(_array2, item => Console.Write($"{item} "));
   }
}