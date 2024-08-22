using StringHandling.Prefix;

namespace StringHandling.Tests;

[TestFixture]
public class TrieTests
{
   [SetUp]
   public void TrieSetup()
   {
      _trie = new Trie<int>();
      Assert.That(_trie.IsEmpty, Is.True);
      for (var i = 0; i < _keys.Length; i++)
      {
         // TONOTE: Since int's default value is equal to 0, start with 1
         var keyValue = _keys[i];
         _trie[keyValue] = i + 1;
      }

      Assert.That(_trie.IsEmpty, Is.False);
   }

   private Trie<int> _trie = null!;

   private readonly string[] _keys =
   {
      "she",
      "sells",
      "sea",
      "shells",
      "by",
      "the",
      "sea", // TONOTE: duplicated key
      "shore"
   };

   [Test]
   public void TrieCheckContentTest()
   {
      var expectedValues = new HashSet<string>();
      foreach (var key in _keys)
      {
         expectedValues.Add(key);
      }

      var actualValues = new HashSet<string>();
      foreach (var key in _trie.Keys)
      {
         var value = _trie[key];
         if (value != default)
         {
            actualValues.Add(key);
         }
      }

      var isSameSet = expectedValues.SetEquals(actualValues);
      Assert.That(isSameSet, Is.True);
   }

   [Test]
   public void LongestPrefixTest()
   {
      const string expectedLongestPrefix = "shells";
      var actualLongestPrefix = _trie.GetLongestPrefixOf("shellsort");
      Assert.That(actualLongestPrefix, Is.EqualTo(expectedLongestPrefix));
   }

   [Test]
   public void NotFoundLongestPrefixTest()
   {
      var actualNullPrefix = _trie.GetLongestPrefixOf("quicksort");
      Assert.That(string.IsNullOrEmpty(actualNullPrefix), Is.True);
   }

   [Test]
   public void GetKeysWithPrefixTest()
   {
      const string shorPrefix = "shor";
      var expectedShorPrefixKeys = new HashSet<string>
      {
         "shor",
         "shore"
      };

      var actualShorPrefixKeys = new HashSet<string>();
      foreach (var str in _trie.GetKeysWithPrefix(shorPrefix))
      {
         actualShorPrefixKeys.Add(str);
      }

      var isSamePrefixKeys = expectedShorPrefixKeys.SetEquals(actualShorPrefixKeys);
      Assert.That(isSamePrefixKeys, Is.True);
   }

   [Test]
   public void GetMatchedKeysTest()
   {
      var expectedMatchKeys = new HashSet<string> { "shells" };
      var actualMatchKeys = new HashSet<string>();
      const string matchPattern = ".he.l.";
      foreach (var matchedKey in _trie.GetMatchedKeys(matchPattern))
      {
         actualMatchKeys.Add(matchedKey);
      }

      var isSameMatch = expectedMatchKeys.SetEquals(actualMatchKeys);
      Assert.That(isSameMatch, Is.True);
   }

   [Test]
   public void DeleteTest()
   {
      var expectedSize = _keys.Length - 1;
      Assert.That(_trie.Size, Is.EqualTo(expectedSize));
      const string byKey = "by";
      Assert.That(_trie.Contains(byKey), Is.True);
      _trie.Delete(byKey);
      Assert.That(_trie.Contains(byKey), Is.False);
      Assert.That(_trie.Size, Is.EqualTo(expectedSize - 1));
   }
}