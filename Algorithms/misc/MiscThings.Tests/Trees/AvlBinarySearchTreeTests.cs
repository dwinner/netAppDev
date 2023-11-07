using MiscThings.Trees;

namespace MiscThings.Tests.Trees;

[TestFixture]
public class AvlBinarySearchTreeTests
{
   [SetUp]
   public void SetUp()
   {
      _avlTree = new AvlBinarySearchTree<IntWrapper, string>();
      foreach (var @int in Enumerable.Range(0, TreeSize))
      {
         _avlTree[@int] = $"{@int}";
      }
   }

   private const int TreeSize = 1_000;

   private AvlBinarySearchTree<IntWrapper, string> _avlTree = null!;

   [Test]
   public void AvlTree_ValidTest()
   {
      var isValid = _avlTree.Check(out var errorMsg);
      Assert.That(isValid, Is.True, errorMsg);
   }

   [Test]
   public void AvlTree_IsEmpty()
   {
      Assert.Multiple(() =>
      {
         Assert.That(_avlTree.IsEmpty, Is.False);
         Assert.That(new AvlBinarySearchTree<string, string>().IsEmpty, Is.True);
      });
   }

   [Test]
   public void AvlTree_Size()
   {
      Assert.That(_avlTree.Size, Is.EqualTo(TreeSize));
   }

   [Test]
   public void AvlTree_Height()
   {
      var expectedHeight = (int)Math.Round(Math.Log2(TreeSize), MidpointRounding.ToZero);
      Assert.That(expectedHeight, Is.EqualTo(_avlTree.Height));
   }

   [Test]
   public void AvlTree_GetValue()
   {
      var rnd = new Random(Environment.TickCount);
      var randomKey = rnd.Next(1, TreeSize);
      var expectedValue = $"{randomKey}";
      Assert.That(expectedValue, Is.EqualTo(_avlTree[randomKey]));
   }

   [Test]
   public void AvlTree_SetValue()
   {
      var prevSize = _avlTree.Size;
      var rnd = new Random(Environment.TickCount);
      var randomKey = rnd.Next(TreeSize + 2, 2 * TreeSize);
      var valToInsert = $"{randomKey}";
      Assert.That(_avlTree[randomKey], Is.EqualTo(default));
      _avlTree[randomKey] = valToInsert;
      Assert.That(_avlTree[randomKey], Is.EqualTo(valToInsert));
      Assert.That(_avlTree.Size, Is.EqualTo(prevSize + 1));
   }

   [Test]
   public void AvlTree_Contains()
   {
      var notContains = _avlTree.Contains(-1);
      var contains = _avlTree.Contains(1);
      Assert.That(notContains, Is.False);
      Assert.That(contains, Is.True);
   }

   [Test]
   public void AvlTree_Delete()
   {
      Assert.Throws<ArgumentNullException>(() => _avlTree.Delete(default!));
      for (var i = 0; i < 100; i++)
      {
         Assert.That(_avlTree.Contains(i), Is.True);
         _avlTree.Delete(i);
         Assert.That(_avlTree.Contains(i), Is.False);
      }
   }

   [Test]
   public void AvlTree_DeleteMin()
   {
      var minKey = _avlTree.GetMin();
      Assert.That(_avlTree.Contains(minKey), Is.True);
      _avlTree.DeleteMin();
      Assert.That(_avlTree.Contains(minKey), Is.False);
   }

   [Test]
   public void AvlTree_DeleteMax()
   {
      var maxKey = _avlTree.GetMax();
      Assert.That(_avlTree.Contains(maxKey), Is.True);
      _avlTree.DeleteMax();
      Assert.That(_avlTree.Contains(maxKey), Is.False);
   }

   [Test]
   public void AvlTree_GetMin()
   {
      const int expected = 0;
      var minKey = _avlTree.GetMin();
      Assert.That(minKey.IntValue, Is.EqualTo(expected));
   }

   [Test]
   public void AvlTree_GetMax()
   {
      const int expected = 999;
      var maxKey = _avlTree.GetMax();
      Assert.That(maxKey.IntValue, Is.EqualTo(expected));
   }

   [Test]
   public void AvlTree_GetFloor()
   {
      const int keyToDel = 67;
      const int expectedFloor = 66;
      _avlTree.Delete(keyToDel);
      var floorValue = _avlTree.GetFloor(keyToDel);
      Assert.Multiple(() =>
      {
         Assert.That(floorValue, Is.Not.EqualTo(null));
         Assert.That(floorValue!.IntValue, Is.EqualTo(expectedFloor));
      });
   }

   [Test]
   public void AvlTree_GetCeiling()
   {
      const int keyToDel = 67;
      const int expectedCeil = 68;
      _avlTree.Delete(keyToDel);
      var actual = _avlTree.GetCeiling(keyToDel);
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual!.IntValue, Is.EqualTo(expectedCeil));
   }

   [Test]
   public void AvlTree_Select()
   {
      const int expected = 100;
      const int orderedKey = 100;
      var actual = _avlTree.Select(orderedKey);
      Assert.That(actual!.IntValue, Is.EqualTo(expected));
   }

   [Test]
   public void AvlTree_GetRank()
   {
      const int expected = 100;
      var actual = _avlTree.GetRank(100);
      Assert.That(actual, Is.EqualTo(expected));
   }

   [Test]
   public void AvlTree_GetKeys()
   {
      var actual = _avlTree.GetKeys();
      var expected = new SortedSet<IntWrapper>();
      foreach (var intVal in Enumerable.Range(0, TreeSize))
      {
         expected.Add(intVal);
      }

      var isEqual = expected.SetEquals(actual);
      Assert.That(isEqual, Is.True);
   }

   [Test]
   public void AvlTree_GetKeysInOrder() => AvlTree_GetKeys();

   [Test]
   public void AvlTree_GetKeysLevelOrder()
   {
      var actual = _avlTree.GetKeysLevelOrder();
      var expected = new SortedSet<IntWrapper>();
      foreach (var intVal in Enumerable.Range(0, TreeSize))
      {
         expected.Add(intVal);
      }

      var isEqual = expected.SetEquals(actual);
      Assert.That(isEqual, Is.True);
   }

   [Test]
   public void AvlTree_GetKeysInRange()
   {
      const int lowKey = 100;
      const int highKey = 200;
      var expectedRange = Enumerable.Range(lowKey, highKey - lowKey + 1).ToArray();
      var actualRange = _avlTree.GetKeys(lowKey, highKey).ToArray();
      for (var i = 0; i < actualRange.Length; i++)
      {
         var actual = actualRange[i];
         var expected = expectedRange[i];
         Assert.That((int)actual, Is.EqualTo(expected));
      }
   }

   [Test]
   public void AvlTree_GetSize()
   {
      const int lowKey = 100;
      const int highKey = 200;
      const int expectedSize = highKey - lowKey + 1;
      var actualSize = _avlTree.GetSize(lowKey, highKey);
      Assert.That(actualSize, Is.EqualTo(expectedSize));
   }
}

internal sealed class IntWrapper : IComparable<IntWrapper>, IComparable, IEquatable<IntWrapper>
{
   private static readonly Comparer<IntWrapper> DefaultComparer = Comparer<IntWrapper>.Default;

   public IntWrapper(int intValue) => IntValue = intValue;

   public int IntValue { get; }

   public int CompareTo(object? obj) =>
      ReferenceEquals(null, obj)
         ? 1
         : ReferenceEquals(this, obj)
            ? 0
            : obj is IntWrapper other
               ? CompareTo(other)
               : throw new ArgumentException($"Object must be of type {nameof(IntWrapper)}");

   public int CompareTo(IntWrapper? other) =>
      ReferenceEquals(this, other)
         ? 0
         : ReferenceEquals(null, other)
            ? 1
            : IntValue.CompareTo(other.IntValue);

   public bool Equals(IntWrapper? other)
   {
      if (ReferenceEquals(null, other))
      {
         return false;
      }

      if (ReferenceEquals(this, other))
      {
         return true;
      }

      return IntValue == other.IntValue;
   }

   public override bool Equals(object? obj) =>
      ReferenceEquals(this, obj) || (obj is IntWrapper other && Equals(other));

   public override int GetHashCode() => IntValue;

   public static bool operator ==(IntWrapper? left, IntWrapper? right) => Equals(left, right);

   public static bool operator !=(IntWrapper? left, IntWrapper? right) => !Equals(left, right);

   public static bool operator <(IntWrapper? left, IntWrapper? right) =>
      DefaultComparer.Compare(left, right) < 0;

   public static bool operator >(IntWrapper? left, IntWrapper? right) =>
      DefaultComparer.Compare(left, right) > 0;

   public static bool operator <=(IntWrapper? left, IntWrapper? right) =>
      DefaultComparer.Compare(left, right) <= 0;

   public static bool operator >=(IntWrapper? left, IntWrapper? right) =>
      DefaultComparer.Compare(left, right) >= 0;

   public static explicit operator int(IntWrapper intWrapper) =>
      intWrapper.IntValue;

   public static implicit operator IntWrapper(int intValue) =>
      new(intValue);

   public override string ToString() => IntValue.ToString();
}