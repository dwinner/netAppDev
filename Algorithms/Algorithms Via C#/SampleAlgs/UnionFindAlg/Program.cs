/**
 * Алгоритм объединения/поиска групп пар целых чисел
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnionFindAlg
{
   internal static class Program
   {
      private static readonly string[] PairFiles = { "tinyUF.txt", "mediumUF.txt", "largeUF.txt" };

      private static readonly Action<IList<Tuple<int, int>>> AlgAction = list =>
      {
         IUnionFind unionFind = new UnionFindImpl(list.Count);
         foreach (var numberPair in list.Where(numberPair => !unionFind.Connected(numberPair.Item1, numberPair.Item2)))
         {
            unionFind.Union(numberPair.Item1, numberPair.Item2);
            Console.WriteLine("{0} {1}", numberPair.Item1, numberPair.Item2);
         }
      };

      private static void Main()
      {
         // Решение задачи динамической связности
         var tinyPairList = GetNumberPairs(PairFiles[0]);
         ProfileTiny(tinyPairList);

         var mediumPairList = GetNumberPairs(PairFiles[1]);
         ProfileMedium(mediumPairList);

         var largePairList = GetNumberPairs(PairFiles[2]);
         ProfileLarge(largePairList);
      }

      private static void ProfileLarge(IList<Tuple<int, int>> largeList) => AlgAction(largeList);

      private static void ProfileMedium(IList<Tuple<int, int>> mediumList) => AlgAction(mediumList);

      private static void ProfileTiny(IList<Tuple<int, int>> tinyList) => AlgAction(tinyList);

      private static List<Tuple<int, int>> GetNumberPairs(string aFile)
      {
         var allLines = File.ReadAllLines(aFile);
         var pairs =
            (from tinyLine in allLines
             select tinyLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
               into currentLine
             where currentLine.Length == 2
             let first = int.Parse(currentLine[0].Trim())
             let second = int.Parse(currentLine[1].Trim())
             select Tuple.Create(first, second)).ToList();

         return pairs;
      }      
   }
}