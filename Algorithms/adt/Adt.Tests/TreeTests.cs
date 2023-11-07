using DataStructures.TreeBased;
using MoreLinq;

namespace Adt.Tests;

[TestFixture]
public class TreeTests
{
   [Test]
   public void BTreeIntegrationTest()
   {
      var tree = new BTree<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

      // Count
      const int expectedCount = 10;
      Assert.That(tree, Has.Count.EqualTo(expectedCount));

      // Remove
      var tenRemoved = tree.Remove("10");
      Assert.That(tenRemoved, Is.True);
      var zeroRemoved = tree.Remove("0");
      Assert.Multiple(() =>
      {
         Assert.That(zeroRemoved, Is.False);
         Assert.That(tree, Has.Count.EqualTo(expectedCount - 1));
      });

      // Contains
      var oneContains = tree.Contains("1");
      Assert.That(oneContains, Is.True);
      var zeroContains = tree.Contains("0");
      Assert.That(zeroContains, Is.False);

      // Enumeration
      var expectedList = tree.Select(int.Parse).ToList();
      for (var i = 1; i < expectedList.Count; i++)
      {
         var prevVal = expectedList[i - 1];
         var currentVal = expectedList[i];
         Assert.That(currentVal, Is.GreaterThan(prevVal));
      }

      Console.Write("BTree enumeration: ");
      expectedList.ForEach(item => Console.Write($"{item} "));

      // Clear
      tree.Clear();
      Assert.That(tree, Is.Empty);
   }

   [Test]
   public void AvlTreeIntegrationTest()
   {
      var tree = new AvlTree<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

      // Count test
      const int expectedCount = 10;
      Assert.That(tree, Has.Count.EqualTo(expectedCount));

      // Head test
      var headValue = tree.Head!.Value;
      Assert.That(headValue, Is.EqualTo("4"));

      // Contains
      var containsTen = tree.Contains("10");
      Assert.That(containsTen, Is.True);
      var containsZero = tree.Contains("0");
      Assert.That(containsZero, Is.False);

      // Remove
      var tenRemoved = tree.Remove("10");
      Assert.Multiple(() =>
      {
         Assert.That(tenRemoved, Is.True);
         Assert.That(tree, Has.Count.EqualTo(expectedCount - 1));
      });
      var zeroRemoved = tree.Remove("0");
      Assert.Multiple(() =>
      {
         Assert.That(zeroRemoved, Is.False);
         Assert.That(tree, Has.Count.EqualTo(expectedCount - 1));
      });

      // Enumeration
      var expected = new List<string>(expectedCount);
      tree.ForEach(item => expected.Add(item));
      for (var i = 1; i < expected.Count; i++)
      {
         var previous = expected[i - 1];
         var current = expected[i];
         Assert.That(string.Compare(current, previous, StringComparison.Ordinal), Is.GreaterThan(0));
      }

      Console.Write("AVL Enumeration:");
      expected.ForEach(item => Console.Write($"{item} "));
      Console.WriteLine();

      // Clear
      tree.Clear();
      Assert.Multiple(() =>
      {
         Assert.That(tree.Head, Is.EqualTo(default(AvlTreeNode<string>)));
         Assert.That(tree, Is.Empty);
      });
   }

   [Test]
   public void BinaryTreeIntegrationTest()
   {
      var tree = new BinaryTree<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

      // Count test
      const int expectedCount = 10;
      Assert.That(tree, Has.Count.EqualTo(expectedCount));

      // Contains test
      var containsOne = tree.Contains("1");
      Assert.That(containsOne, Is.True);
      var containsZero = tree.Contains("0");
      Assert.That(containsZero, Is.False);

      // Remove test
      var tenRemoved = tree.Remove("10");
      Assert.Multiple(() =>
      {
         Assert.That(tenRemoved, Is.True);
         Assert.That(tree, Has.Count.EqualTo(expectedCount - 1));
      });
      var zeroRemoved = tree.Remove("0");
      Assert.Multiple(() =>
      {
         Assert.That(zeroRemoved, Is.False);
         Assert.That(tree, Has.Count.EqualTo(expectedCount - 1));
      });

      // PreOrderTraversal
      var preOrderList = new List<string>(expectedCount);
      tree.PreOrderTraversal(item => preOrderList.Add(item));
      Console.Write($"{nameof(tree.PreOrderTraversal)}:");
      preOrderList.ForEach(item => Console.Write($" {item}"));
      Console.WriteLine();

      // PostOrderTraversal
      var postOrderList = new List<string>(expectedCount);
      tree.PostOrderTraversal(item => postOrderList.Add(item));
      Console.Write($"{nameof(tree.PostOrderTraversal)}:");
      postOrderList.ForEach(item => Console.Write($" {item}"));
      Console.WriteLine();

      // InOrderTraversal
      var inOrderList = new List<string>(expectedCount);
      tree.InOrderTraversal(item => inOrderList.Add(item));
      Console.Write($"{nameof(tree.InOrderTraversal)}");
      inOrderList.ForEach(item => Console.Write($" {item}"));
      Console.WriteLine();

      // Clear
      tree.Clear();
      Assert.That(tree, Is.Empty);
   }
}