namespace BinarySearchSample.Test;

public class BinarySearchTests
{
   private readonly int[] _intArray = Enumerable.Range(1, 1_000_000).ToArray();

   [SetUp]
   public void Setup()
   {
      Array.Sort(_intArray);
   }

   [Test]
   public void BinarySearch_FoundElement()
   {
      const int key = 30000;
      var foundIndex = BinarySearch.Rank(key, _intArray);
      Assert.That(foundIndex, Is.Not.EqualTo(-1));
   }

   [Test]
   public void BinarySearch_NotFound()
   {
      const int key = 3_000_000;
      var foundIndex = BinarySearch.Rank(key, _intArray);
      Assert.That(foundIndex, Is.EqualTo(-1));
   }
}