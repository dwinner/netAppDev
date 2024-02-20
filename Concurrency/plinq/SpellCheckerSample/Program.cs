var wordLookupFile = Path.Combine(Path.GetTempPath(), "WordLookup.txt");

if (!File.Exists(wordLookupFile)) // Contains about 150,000 words
{
   File.WriteAllText(wordLookupFile,
      await new HttpClient().GetStringAsync(
         "http://www.albahari.com/ispell/allwords.txt").ConfigureAwait(false));
}

var wordLookup = new HashSet<string>(
   File.ReadAllLines(wordLookupFile),
   StringComparer.InvariantCultureIgnoreCase);

var random = new Random();
var wordList = wordLookup.ToArray();

var wordsToTest = Enumerable.Range(0, 1_000_000)
   .Select(i => wordList[random.Next(0, wordList.Length)])
   .ToArray();

wordsToTest[12345] = "woozsh"; // Introduce a couple
wordsToTest[23456] = "wubsie"; // of spelling mistakes.

var query = wordsToTest
   .AsParallel()
   .Select((word, index) => (word, index))
   .Where(iword => !wordLookup.Contains(iword.word))
   .OrderBy(iword => iword.index);

query.ForAll(valTuple =>
{
   var (word, index) = valTuple;
   Console.WriteLine($"{word} at {index}");
});