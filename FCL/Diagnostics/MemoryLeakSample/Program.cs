/*
 * This program is intentially written to perform poorly.
 * You can run it while experimenting with the diagnostics techniques.
 */

using System.Diagnostics;

namespace MemoryLeakSample;

internal static class Program
{
   private static readonly Dictionary<string, int> CacheThatNeverCleansUp = new();

   private static readonly Random Rnd = new();

   private static void Main()
   {
      // The diagnostic tools need our process ID:
      Console.WriteLine($"Our process ID {Process.GetCurrentProcess().Id}");
      MemoryLeak();
   }

   private static void MemoryLeak()
   {
      // Pretend this is an expensive calculation worth caching
      int CalculateSentenceScore(string sentence)
      {
         var watch = Stopwatch.StartNew();
         try
         {
            if (CacheThatNeverCleansUp.TryGetValue(sentence, out var score))
            {
               return score;
            }

            var calculatedScore = sentence.Split(' ').Length;
            CacheThatNeverCleansUp.Add(sentence, calculatedScore);
            return calculatedScore;
         }
         finally
         {
            MyEventCounterSource.Log.Request(sentence, watch.ElapsedMilliseconds);
         }
      }

      while (true) // Simulate e.g. a web service that keeps taking requests
      {
         var input = RandomSentence();
         var score = CalculateSentenceScore(input);
         // A web service might return the score to a caller    
      }
   }

   private static string RandomSentence()
   {
      const string alpha = "abcdefghijklmnopqrstuvwxyz";
      var words = new List<string>();
      var numWords = Rnd.Next(2, 15);
      for (var w = 0; w < numWords; w++)
      {
         var wordLen = Rnd.Next(1, 10);
         words.Add(new string(Enumerable.Repeat(alpha, wordLen)
            .Select(str => str[Rnd.Next(str.Length)]).ToArray()));
      }

      return string.Join(' ', words);
   }

   private static int[] LongRandomArray(int size)
   {
      return Enumerable.Repeat(0, size).Select(i => Rnd.Next()).ToArray();
   }
}