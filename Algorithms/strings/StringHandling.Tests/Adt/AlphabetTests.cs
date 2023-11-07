namespace StringHandling.Tests.Adt;

[TestFixture]
public class AlphabetTests : StringTestsBase
{
   [Test]
   public void AlphabetTest()
   {
      const string base64Expected = "NowIsTheTimeForAllGoodMen";
      var encoded1 = StringHandling.Adt.Alphabet.Base64.ToIndices(base64Expected);
      var base64Actual = StringHandling.Adt.Alphabet.Base64.ToChars(encoded1);
      Assert.That(base64Actual, Is.EqualTo(base64Expected));

      const string dnaExpected = "AACGAACGGTTTACCCCG";
      var encoded2 = StringHandling.Adt.Alphabet.Dna.ToIndices(dnaExpected);
      var dnaActual = StringHandling.Adt.Alphabet.Dna.ToChars(encoded2);
      Assert.That(dnaActual, Is.EqualTo(dnaExpected));

      const string decimalExpected = "01234567890123456789";
      var encoded3 = StringHandling.Adt.Alphabet.Decimal.ToIndices(decimalExpected);
      var decimalActual = StringHandling.Adt.Alphabet.Decimal.ToChars(encoded3);
      Assert.That(decimalActual, Is.EqualTo(decimalExpected));
   }

   [TestCase("ABCDR", "https://algs4.cs.princeton.edu/50strings/abra.txt")]
   [TestCase("0123456789", "https://algs4.cs.princeton.edu/50strings/pi.txt")]
   public async Task CountTest(string alphaSet, string contentUri)
   {
      var alpha = new StringHandling.Adt.Alphabet(alphaSet);
      var radix = alpha.Radix;
      var content = await GetContentAsync(contentUri);
      var count = new int[radix];
      Array.Fill(count, 0);
      foreach (var charIndex in
               from charToCount in content
               where alpha.Contains(charToCount)
               select alpha.ToIndex(charToCount))
      {
         count[charIndex]++;
      }

      for (var c = 0; c < radix; c++)
      {
         Console.WriteLine($"{alpha.ToChar(c)} {count[c]}");
      }
   }
}