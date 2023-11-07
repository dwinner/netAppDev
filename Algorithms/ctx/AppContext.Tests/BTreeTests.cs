using AppContext.DbIdx;

namespace AppContext.Tests;

[TestFixture]
public class BTreeTests
{
   private const int Degree = 2;

   private readonly int[] _testKeyData = { 10, 20, 30, 50 };
   private readonly int[] _testPointerData = { 50, 60, 40, 20 };

   [Test]
   public void CreateBTree()
   {
      var btree = new BTree<int, int>(Degree);
      var root = btree.Root;

      Assert.That(root, Is.Not.Null);

      Assert.Multiple(() =>
      {
         Assert.That(root.Entries, Is.Not.Null);
         Assert.That(root.Children, Is.Not.Null);
      });

      Assert.Multiple(() =>
      {
         Assert.That(root.Entries, Is.Empty);
         Assert.That(root.Children, Is.Empty);
      });
   }

   [Test]
   public void InsertOneNode()
   {
      var btree = new BTree<int, int>(Degree);
      InsertTestDataAndValidateTree(btree, 0);
      Assert.That(btree.Height, Is.EqualTo(1));
   }

   [Test]
   public void InsertMultipleNodesToSplit()
   {
      var btree = new BTree<int, int>(Degree);
      for (var i = 0; i < _testKeyData.Length; i++)
      {
         InsertTestDataAndValidateTree(btree, i);
      }

      Assert.That(btree.Height, Is.EqualTo(2));
   }

   [Test]
   public void DeleteNodes()
   {
      var btree = new BTree<int, int>(Degree);
      for (var i = 0; i < _testKeyData.Length; i++)
      {
         InsertTestData(btree, i);
      }

      for (var i = 0; i < _testKeyData.Length; i++)
      {
         btree.Delete(_testKeyData[i]);
         ValidateTree(btree.Root, Degree, _testKeyData.Skip(i + 1).ToArray());
      }

      Assert.That(btree.Height, Is.EqualTo(1));
   }

   [Test]
   public void DeleteNonExistingNode()
   {
      var btree = new BTree<int, int>(Degree);
      for (var i = 0; i < _testKeyData.Length; i++)
      {
         InsertTestData(btree, i);
      }

      btree.Delete(99999);
      ValidateTree(btree.Root, Degree, _testKeyData.ToArray());
   }

   [Test]
   public void SearchNodes()
   {
      var btree = new BTree<int, int>(Degree);
      for (var i = 0; i < _testKeyData.Length; i++)
      {
         InsertTestData(btree, i);
         SearchTestData(btree, i);
      }
   }

   [Test]
   public void SearchNonExistingNode()
   {
      var btree = new BTree<int, int>(Degree);

      // search an empty tree
      var nonExisting = btree.Search(9999);
      Assert.That(nonExisting, Is.Null);

      for (var i = 0; i < _testKeyData.Length; i++)
      {
         InsertTestData(btree, i);
         SearchTestData(btree, i);
      }

      // search a populated tree
      nonExisting = btree.Search(9999);
      Assert.That(nonExisting, Is.Null);
   }

   private void InsertTestData(BTree<int, int> btree, int testDataIndex)
   {
      btree.Insert(_testKeyData[testDataIndex], _testPointerData[testDataIndex]);
   }

   private void InsertTestDataAndValidateTree(BTree<int, int> btree, int testDataIndex)
   {
      btree.Insert(_testKeyData[testDataIndex], _testPointerData[testDataIndex]);
      ValidateTree(btree.Root, Degree, _testKeyData.Take(testDataIndex + 1).ToArray());
   }

   private void SearchTestData(BTree<int, int> btree, int testKeyDataIndex)
   {
      for (var i = 0; i <= testKeyDataIndex; i++)
      {
         var item = _testKeyData[i];
         var entry = btree.Search(item);
         Assert.Multiple(() =>
         {
            Assert.That((object)item, Is.Not.Null);
            Assert.That(entry!.Key, Is.EqualTo(item));
            Assert.That(entry.Pointer, Is.EqualTo(_testPointerData[i]));
         });
      }
   }

   private static void ValidateTree(BTreeNode<int, int> tree, int degree, params int[] expectedKeys)
   {
      var foundKeys = new Dictionary<int, List<BTreeEntry<int, int>>?>();
      ValidateSubtree(tree, tree, degree, int.MinValue, int.MaxValue, foundKeys);
      Assert.That(expectedKeys.Except(foundKeys.Keys).Count(), Is.EqualTo(0));
      foreach (var keyValuePair in foundKeys)
      {
         Assert.That(keyValuePair.Value, Has.Count.EqualTo(1));
      }
   }

   private static void UpdateFoundKeys(IDictionary<int, List<BTreeEntry<int, int>>?> foundKeys,
      BTreeEntry<int, int> entry)
   {
      if (!foundKeys.TryGetValue(entry.Key, out var foundEntries))
      {
         foundEntries = new List<BTreeEntry<int, int>>();
         foundKeys.Add(entry.Key, foundEntries);
      }

      foundEntries?.Add(entry);
   }

   private static void ValidateSubtree(BTreeNode<int, int> root, BTreeNode<int, int> node, int degree, int nodeMin,
      int nodeMax,
      IDictionary<int, List<BTreeEntry<int, int>>?> foundKeys)
   {
      if (root != node)
      {
         Assert.That(node.Entries, Has.Count.GreaterThanOrEqualTo(degree - 1));
         Assert.That(node.Entries, Has.Count.LessThanOrEqualTo(2 * degree - 1));
      }

      for (var i = 0; i <= node.Entries.Count; i++)
      {
         var subtreeMin = nodeMin;
         var subtreeMax = nodeMax;

         if (i < node.Entries.Count)
         {
            var entry = node.Entries[i];
            UpdateFoundKeys(foundKeys, entry);
            Assert.That(entry.Key >= nodeMin && entry.Key <= nodeMax, Is.True);
            subtreeMax = entry.Key;
         }

         if (i > 0)
         {
            subtreeMin = node.Entries[i - 1].Key;
         }

         if (!node.IsLeaf)
         {
            Assert.That(node.Children, Has.Count.GreaterThanOrEqualTo(degree));
            ValidateSubtree(root, node.Children[i], degree, subtreeMin, subtreeMax, foundKeys);
         }
      }
   }
}