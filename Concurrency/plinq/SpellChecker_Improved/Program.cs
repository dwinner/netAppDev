using System.Net;

namespace SpellChecker_Improved;

internal static class Program
{
   private static void Main()
   {
      var wordLookupFile = Path.Combine(Path.GetTempPath(), "WordLookup.txt");

      if (!File.Exists(wordLookupFile)) // Contains about 150,000 words
      {
         new WebClient().DownloadFile(
            "http://www.albahari.com/ispell/allwords.txt", wordLookupFile);
      }

      var wordLookup = new HashSet<string>(
         File.ReadAllLines(wordLookupFile),
         StringComparer.InvariantCultureIgnoreCase);

      var wordList = wordLookup.ToArray();

      // Here, we're using ThreadLocal to generate a thread-safe random number generator,
      // so we can parallelize the building of the wordsToTest array.
      var localRandom = new ThreadLocal<Random>
         (() => new Random(Guid.NewGuid().GetHashCode()));

      var wordsToTest = Enumerable.Range(0, 1_000_000)
         .AsParallel()
         .Select(_ => wordList[localRandom.Value.Next(0, wordList.Length)])
         .ToArray();

      wordsToTest[12345] = "woozsh"; // Introduce a couple
      wordsToTest[23456] = "wubsie"; // of spelling mistakes.

      var query = wordsToTest
         .AsParallel()
         .Select((word, index) => new IndexedWord { Word = word, Index = index })
         .Where(iword => !wordLookup.Contains(iword.Word))
         .OrderBy(iword => iword.Index);

      query.ForAll(word => { Console.WriteLine($"{word.Word} at {word.Index}"); });
   }
}

internal struct IndexedWord
{
   public string Word;
   public int Index;
}