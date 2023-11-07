using StringHandling.Prefix;

namespace StringHandling.Tests;

[TestFixture]
public class TernarySearchTrieTests
{
   [SetUp]
   public void TrieInit()
   {
      _trie = new TernarySearchTrie<int>();
      foreach (var (key, val) in _stMap)
      {
         _trie[val] = key;
      }
   }

   private const string ValidKey = "sells";
   private const string NonValidKey = "abra";

   private TernarySearchTrie<int> _trie = null!;

   private readonly Dictionary<int, string> _stMap = new()
   {
      [1] = "she",
      [2] = "sells",
      [3] = "sea",
      [4] = "shells",
      [5] = "by",
      [6] = "the",
      [7] = "sea",
      [8] = "shore"
   };

   [Test]
   public void SizeTest()
   {
      const int expectedSize = 7;
      Assert.That(_trie.Size, Is.EqualTo(expectedSize));
   }

   [Test]
   public void ContainsTest()
   {
      Assert.Multiple(() =>
      {
         Assert.That(_trie.Contains(ValidKey), Is.True);
         Assert.That(_trie.Contains(NonValidKey), Is.False);
      });
   }

   [Test]
   public void GetKeyTest()
   {
      var validValue = _trie[ValidKey];
      var nonValidValue = _trie[NonValidKey];
      Assert.Multiple(() =>
      {
         Assert.That(validValue, Is.Not.EqualTo(default(int)));
         Assert.That(nonValidValue, Is.EqualTo(default(int)));
      });
   }

   [Test]
   public void PutKeyTest()
   {
      var nonValidValue = _trie[NonValidKey];
      Assert.That(nonValidValue, Is.EqualTo(default(int)));

      // Put the new value
      var updatedSize = _trie.Size + 1;
      _trie[NonValidKey] = updatedSize;
      Assert.Multiple(() =>
      {
         // Check the new value
         Assert.That(_trie.Size, Is.EqualTo(updatedSize));
         Assert.That(_trie[NonValidKey], Is.Not.EqualTo(default(int)));
         Assert.That(_trie.Contains(NonValidKey), Is.True);
      });

      // Remove just added value
      _trie[NonValidKey] = default;
      Assert.Multiple(() =>
      {
         Assert.That(_trie.Size, Is.EqualTo(updatedSize - 1));
         Assert.That(_trie[NonValidKey], Is.EqualTo(default(int)));
         Assert.That(_trie.Contains(NonValidKey), Is.False);
      });
   }

   [Test]
   public void GetLongestPrefixOfTest()
   {
      const string query = "shellsort";
      const string expectedLongestPrefix = "shells";
      var actualLongestPrefix = _trie.GetLongestPrefixOf(query);
      Assert.That(actualLongestPrefix, Is.EqualTo(expectedLongestPrefix));
   }

   [Test]
   public void GetKeysTest()
   {
      var expected = new HashSet<string>();
      foreach (var key in _trie.GetKeys())
      {
         expected.Add(key);
      }

      var actual = new HashSet<string>(_stMap.Values);
      Assert.That(actual.SetEquals(expected), Is.True);
   }

   [Test]
   public void GetKeysWithPrefixTest()
   {
      const string prefix = "shor";
      const string expected = "shore";
      var pfxKeys = _trie.GetKeysWithPrefix(prefix).ToArray();
      Assert.That(pfxKeys is [expected], Is.True);
      var nvldKeys = _trie.GetKeysWithPrefix(NonValidKey);
      Assert.That(!nvldKeys.Any(), Is.True);
   }

   [Test]
   public void GetKeysThatMatchTest()
   {
      const string pattern = ".he.l.";
      const string expected = "shells";
      var matched = _trie.GetKeysThatMatch(pattern).ToArray();
      Assert.That(matched is [expected], Is.True);
      var invalidKeys = _trie.GetKeysWithPrefix(NonValidKey);
      Assert.That(!invalidKeys.Any(), Is.True);
   }
}