using MiscThings.Trees;

namespace MiscThings.Tests.Trees;

[TestFixture]
public class RedBlackBinarySearchTreeTests
{
   [SetUp]
   public void Setup()
   {
      _sortedMap = new RedBlackBinarySearchTree<string, Lazy<int>>();
      for (var i = 0; i < MapSize; i++)
      {
         var key = $"{i}";
         var localI = i;
         var value = new Lazy<int>(() => localI);
         _sortedMap[key] = value;
      }
   }

   private const int MapSize = 10_000;
   private RedBlackBinarySearchTree<string, Lazy<int>> _sortedMap = new();

   [Test]
   public void RedBlackTree_CheckBalanced()
   {
      var checkResult = _sortedMap.Check(out var errorMessage);
      Assert.That(checkResult, Is.True, errorMessage);
   }

   [Test]
   public void RedBlackTree_CheckBalanced_AfterRemoving()
   {
      Random rnd = new(Environment.TickCount);
      var randomKeysToRemove = new HashSet<string>();
      for (var i = 0; i < MapSize; i++)
      {
         var randomNum = rnd.Next(0, MapSize);
         randomKeysToRemove.Add($"{randomNum}");
      }

      // Delete random keys
      foreach (var key in randomKeysToRemove)
      {
         _sortedMap.Delete(key);
      }

      // check tree balance
      var isValidTree = _sortedMap.Check(out var errorMessage);
      Assert.That(isValidTree, Is.True, errorMessage);
   }

   [Test]
   public void RedBlackTree_TestHeight()
   {
      var expectedHeight = Math.Round(Math.Log2(MapSize), MidpointRounding.ToPositiveInfinity) + 1;
      var actualHeight = _sortedMap.Height;
      Assert.That(actualHeight, Is.EqualTo(expectedHeight));
   }

   [Test]
   public void RedBlackTree_TestSize()
   {
      var actualSize = _sortedMap.Size;
      Assert.That(actualSize, Is.EqualTo(MapSize));
   }

   [Test]
   public void RedBlackTree_TestEmpty()
   {
      Assert.That(_sortedMap.IsEmpty, Is.False);
      var rbMap = new RedBlackBinarySearchTree<string, Lazy<int>>();
      Assert.That(rbMap.IsEmpty, Is.True);
   }

   [Test]
   public void RedBlackTree_TestGetSet()
   {
      var notExistVal = _sortedMap["-1"];
      Assert.That(notExistVal, Is.Null);

      var existVal = _sortedMap["500"];
      Assert.That(existVal, Is.Not.Null);

      var existedValue = existVal!.Value;
      Assert.That(existedValue, Is.EqualTo(500));

      _sortedMap["500"] = null;
      notExistVal = _sortedMap["500"];
      Assert.That(notExistVal, Is.Null);
   }

   [Test]
   public void RedBlackTree_TestKeys()
   {
      var actualKeys = _sortedMap.Keys.ToArray();
      SortedDictionary<string, Lazy<int>> expectedMap = new();
      for (var i = 0; i < MapSize; i++)
      {
         var key = $"{i}";
         var localI = i;
         var value = new Lazy<int>(() => localI);
         expectedMap[key] = value;
      }

      var expectedKeys = expectedMap.Keys.ToArray();
      for (var i = 0; i < actualKeys.Length; i++)
      {
         var actualKey = actualKeys[i];
         var expectedKey = expectedKeys[i];
         Assert.That(actualKey, Is.EqualTo(expectedKey));
      }
   }

   [Test]
   public void RedBlackTree_TestContains()
   {
      const string validKey = "1234";
      const string invalidKey = "-1";

      Assert.That(_sortedMap.Contains(validKey), Is.True);
      Assert.That(_sortedMap.Contains(invalidKey), Is.False);
   }

   [Test]
   public void RedBlackTree_TestDeleteMin()
   {
      var minKey = _sortedMap.GetMin();
      Assert.That(_sortedMap.Contains(minKey), Is.True);

      _sortedMap.DeleteMin();
      Assert.That(_sortedMap.Contains(minKey), Is.False);
   }

   [Test]
   public void RedBlackTree_TestDeleteMax()
   {
      var maxKey = _sortedMap.GetMax();
      Assert.That(_sortedMap.Contains(maxKey), Is.True);

      _sortedMap.DeleteMax();
      Assert.That(_sortedMap.Contains(maxKey), Is.False);
   }

   [Test]
   public void RedBlackTree_TestDelete()
   {
      string[] keys = { "1", "2", "3", "4", "5" };
      Array.ForEach(keys, key =>
      {
         Assert.That(_sortedMap.Contains(key), Is.True);
         _sortedMap.Delete(key);
         Assert.That(_sortedMap.Contains(key), Is.False);
      });
   }

   [Test]
   public void RedBlackTree_TestGetMin()
   {
      const string expectedMin = "0";
      var actualMin = _sortedMap.GetMin();
      Assert.That(actualMin, Is.EqualTo(expectedMin));
   }

   [Test]
   public void RedBlackTree_TestGetMax()
   {
      const string expectedMax = "9999";
      var actualMax = _sortedMap.GetMax();
      Assert.That(actualMax, Is.EqualTo(expectedMax));
   }

   [Test]
   public void RedBlackTree_TestGetFloor()
   {
      const int basedForFloor = 9995;
      var keyToDel = $"{basedForFloor}";
      var expectedFloor = $"{basedForFloor - 1}";
      _sortedMap.Delete(keyToDel);
      var actualFloor = _sortedMap.GetFloor(keyToDel);
      Assert.That(expectedFloor, Is.EqualTo(actualFloor));
   }

   [Test]
   public void RedBlackTree_TestGetCeiling()
   {
      const int basedForCeiled = 9995;
      var keyToDel = $"{basedForCeiled}";
      var expectedCeiled = $"{basedForCeiled + 1}";
      _sortedMap.Delete(keyToDel);
      var actualCeiled = _sortedMap.GetCeiling(keyToDel);
      Assert.That(expectedCeiled, Is.EqualTo(actualCeiled));
   }

   [Test]
   public void RedBlackTree_TestSelect()
   {
      // There are 10 keys ordered before the key #1006
      const string statKey = "1006";
      const int rank = 10;
      var actualSelected = _sortedMap.Select(rank);
      Assert.That(actualSelected, Is.EqualTo(statKey));
   }

   [Test]
   public void RedBlackTree_TestRank()
   {
      // The key #1006 ordered as the 10th
      const string statKey = "1006";
      const int rank = 10;
      var actualRank = _sortedMap.GetRank(statKey);
      Assert.That(actualRank, Is.EqualTo(rank));
   }

   [Test]
   public void RedBlackTree_TestGetKeys()
   {
      const int start = 1000;
      const int end = 1009;
      SortedSet<string> expected = new();
      for (var i = start; i <= end; i++)
      {
         expected.Add(i.ToString());
      }

      var startKey = start.ToString();
      var endKey = end.ToString();
      var rangeKeys = _sortedMap.GetKeys(startKey, endKey).ToArray();
      SortedSet<string> actual = new(rangeKeys);

      var isEquals = expected.SetEquals(actual);
      Assert.That(isEquals, Is.True);
   }

   [Test]
   public void RedBlackTree_TestGetSize()
   {
      const int start = 1000;
      const int end = 1009;
      const int expectedSize = end - start + 1;

      var actualSize = _sortedMap.GetSize(start.ToString(), end.ToString());
      Assert.That(actualSize, Is.EqualTo(expectedSize));
   }
}