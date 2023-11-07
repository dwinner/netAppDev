using MoreLinq;

namespace Searching.Algs.Tests;

[TestFixture]
public class SetTests
{
   [Test]
   public void SetTest()
   {
      var set = new Set<string>
      {
         "www.cs.princeton.edu",
         "www.cs.princeton.edu",
         "www.princeton.edu",
         "www.math.princeton.edu",
         "www.yale.edu",
         "www.amazon.com",
         "www.simpsons.com",
         "www.stanford.edu",
         "www.google.com",
         "www.ibm.com",
         "www.apple.com",
         "www.slashdot.com",
         "www.whitehouse.gov",
         "www.espn.com",
         "www.snopes.com",
         "www.movies.com",
         "www.cnn.com",
         "www.iitb.ac.in"
      };

      Assert.Multiple(() =>
      {
         Assert.That(set.Contains("www.cs.princeton.edu"), Is.True);
         Assert.That(set.Contains("www.harvardsucks.com"), Is.False);
         Assert.That(set.Contains("www.simpsons.com"), Is.True);
      });

      var ceiling1 = set.GetCeiling("www.simpsonr.com");
      var ceiling2 = set.GetCeiling("www.simpsons.com");
      var ceiling3 = set.GetCeiling("www.simpsont.com");

      Console.WriteLine($"{nameof(ceiling1)}: {ceiling1}");
      Console.WriteLine($"{nameof(ceiling2)}: {ceiling2}");
      Console.WriteLine($"{nameof(ceiling3)}: {ceiling3}");

      Console.WriteLine();

      var floor1 = set.GetFloor("www.simpsonr.com");
      var floor2 = set.GetFloor("www.simpsons.com");
      var floor3 = set.GetFloor("www.simpsont.com");
      Console.WriteLine($"{nameof(floor1)}: {floor1}");
      Console.WriteLine($"{nameof(floor2)}: {floor2}");
      Console.WriteLine($"{nameof(floor3)}: {floor3}");

      Console.WriteLine();

      set.ForEach(key => Console.WriteLine(key));
      var set2 = new Set<string>(set);
      Assert.That(set, Is.Not.EqualTo(set2));
   }

   [Test]
   public void DeDupTest()
   {
      var set = new Set<string>
      {
         "it",
         "was",
         "the",
         "best",
         "of",
         "times",
         "it",
         "was",
         "the",
         "worst",
         "of",
         "times"
      };

      var aggrStr = set.Aggregate(string.Empty, (current, word) => current + $"{word} ");
      Console.WriteLine($"Sentence: {aggrStr}");
   }
}