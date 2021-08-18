/**
 * А правда ли StringBuilder эффективнее?! Проверим...
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HowToCSharp.ch07.StringBuilderTime
{
   class Program
   {
      static readonly Random Rand = new Random();
      private const int DefaultWordsNumber = 4096;

      static void Main(string[] args)
      {
         int numWords;
         if (args.Length < 1 || !int.TryParse(args[0], out numWords))
         {
            numWords = DefaultWordsNumber;
         }

         double[] timings = new double[3];
         Console.WriteLine("Generating {0:N0} random words...", numWords);
         string[] wordList = GenerateWords(numWords);

         Console.WriteLine("Timing AppendWithStringConcatenation");
         Stopwatch watch = new Stopwatch();
         watch.Start();
         string result = AppendWithStringConcatenation(wordList);
         watch.Stop();
         timings[0] = watch.Elapsed.TotalSeconds;

         watch.Reset();

         Console.WriteLine("Timing AppendWithStringBuilder");
         watch.Start();
         result = AppendWithStringBuilder(wordList);
         watch.Stop();
         timings[1] = watch.Elapsed.TotalSeconds;

         watch.Reset();
         int totalLength = wordList.Sum(s => s.Length);
         Console.WriteLine("Timing AppendWithStringBuilder (preallocate)");
         watch.Start();
         result = AppendWithStringBuilderPreallocate(wordList, totalLength);
         watch.Stop();
         timings[2] = watch.Elapsed.TotalSeconds;
         Console.WriteLine("Timings (s): \t{0}\t{1}\t{2}", timings[0], timings[1], timings[2]);

         Console.ReadKey();
      }

      private static string AppendWithStringBuilderPreallocate(IEnumerable<string> wordList, int totalLength)
      {
         StringBuilder sb = new StringBuilder(totalLength);
         foreach (string s in wordList)
         {
            sb.Append(s);
         }
         return sb.ToString();
      }

      private static string AppendWithStringBuilder(IEnumerable<string> wordList)
      {
         StringBuilder sb = new StringBuilder();
         foreach (string s in wordList)
         {
            sb.Append(s);
         }
         return sb.ToString();
      }

      private static string AppendWithStringConcatenation(IEnumerable<string> wordList)
      {
         return wordList.Aggregate("", (current, s) => current + s);
      }

      private static string[] GenerateWords(int numWords)
      {
         string[] list = new string[numWords];
         for (int i = 0; i < numWords; i++)
         {
            list[i] = GenerateWord(Rand.Next(3, 12));
         }
         return list;
      }

      private static string GenerateWord(int length)
      {
         char[] chars = new char[length];
         for (int i = 0; i < chars.Length; i++)
         {
            chars[i] = (char)Rand.Next('a', 'z');
         }
         return new string(chars);
      }
   }
}
