using static MiscThings.Sorting.InsertionX;
using static MiscThings.Tests.Sorting.SortSources;

namespace MiscThings.Tests.Sorting;

[TestFixture]
public class InsertionXTests
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
   public void InsertionXTest()
   {
      Sort(_array1);
      Array.ForEach(_array1, item => Console.Write($"{item} "));
      Console.WriteLine();
      Sort(_array2);
      Array.ForEach(_array2, item => Console.Write($"{item} "));
   }
}