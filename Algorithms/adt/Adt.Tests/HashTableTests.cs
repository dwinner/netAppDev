using DataStructures.HashBased;

namespace Adt.Tests;

[TestFixture]
public class HashTableTests
{
   [SetUp]
   public void Init()
   {
      _hashTable = new HashTable<string, int>();
      foreach (var item in Enumerable.Range(0, ChunkLen))
      {
         _hashTable.Add(item.ToString(), item);
      }
   }

   private const int ChunkLen = 10000;
   private HashTable<string, int> _hashTable = null!;

   [Test]
   public void HashTableGrowingTest()
   {
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen));

      // Grow one more time
      foreach (var item in Enumerable.Range(ChunkLen, ChunkLen))
      {
         _hashTable.Add(item.ToString(), item);
      }

      // Check the count once again
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen * 2));
   }

   [Test]
   public void HashTableAddTest()
   {
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen));
      Assert.Throws<ArgumentException>(() => _hashTable.Add("0", 0));
      _hashTable.Add("-1", -1);
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen + 1));
   }

   [Test]
   public void HashTableRemoveTest()
   {
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen));
      var zeroRemoved = _hashTable.Remove("0");
      Assert.Multiple(() =>
      {
         Assert.That(zeroRemoved, Is.True);
         Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen - 1));
      });

      var nonExRemoved = _hashTable.Remove("12345");
      Assert.That(nonExRemoved, Is.False);
   }

   [Test]
   public void HashTableGetSetKeyTest()
   {
      var zeroValue = _hashTable["0"];
      var oneValue = _hashTable["1"];
      Assert.Multiple(() =>
      {
         Assert.That(zeroValue, Is.EqualTo(0));
         Assert.That(oneValue, Is.EqualTo(1));
      });

      _hashTable["0"] = -1;
      Assert.That(_hashTable["0"], Is.EqualTo(-1));
   }

   [Test]
   public void HashTableTryGetValueTest()
   {
      var tryGetResult = _hashTable.TryGetValue("0", out var val);
      Assert.Multiple(() =>
      {
         Assert.That(val, Is.EqualTo(0));
         Assert.That(tryGetResult, Is.True);
      });

      tryGetResult = _hashTable.TryGetValue("-1", out val);
      Assert.Multiple(() =>
      {
         Assert.That(val, Is.EqualTo(default(int)));
         Assert.That(tryGetResult, Is.False);
      });
   }

   [Test]
   public void HashTableContainsKeyTest()
   {
      var containsResult = _hashTable.ContainsKey("100");
      Assert.That(containsResult, Is.True);
      containsResult = _hashTable.ContainsKey("-1");
      Assert.That(containsResult, Is.False);
   }

   [Test]
   public void HashTableContainsValueTest()
   {
      var containsResult = _hashTable.ContainsValue(0);
      Assert.That(containsResult, Is.True);
      containsResult = _hashTable.ContainsValue(-1);
      Assert.That(containsResult, Is.False);
   }

   [Test]
   public void HashTableGetAllKeysTest()
   {
      var actual = new HashSet<string>(_hashTable.Keys);
      var expected = new HashSet<string>(Enumerable.Range(0, ChunkLen).Select(intVal => intVal.ToString()));
      Assert.That(actual.SetEquals(expected), Is.True);
   }

   [Test]
   public void HashTableGetAllValuesTest()
   {
      var actual = new HashSet<int>(_hashTable.Values);
      var expected = new HashSet<int>(Enumerable.Range(0, ChunkLen));
      Assert.That(actual.SetEquals(expected), Is.True);
   }

   [Test]
   public void HashTableClearTest()
   {
      Assert.That(_hashTable.Count, Is.EqualTo(ChunkLen));
      _hashTable.Clear();
      Assert.That(_hashTable.Count, Is.EqualTo(0));
   }
}