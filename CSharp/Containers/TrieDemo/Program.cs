/*
 * Создание префиксного дерева
 */

using System;
using System.IO;

namespace TrieDemo;

internal static class Program
{
   private static void Main()
   {
      var words = File.ReadAllLines("American-All.txt");

      var trie = new Trie<Info>();
      foreach (var word in words)
      {
         trie.AddValue(word, new Info(word));
      }

      Console.WriteLine("Non-recursive lookup:");
      var info = trie.FindValues("agonize", false);
      foreach (var i in info)
      {
         Console.WriteLine(i.Key);
      }

      Console.WriteLine();
      Console.WriteLine("Recursive lookup:");
      info = trie.FindValues("agonize");
      foreach (var i in info)
      {
         Console.WriteLine(i.Key);
      }

      Console.WriteLine();
      Console.WriteLine("Non-existent lookup:");
      info = trie.FindValues("zzfff");
      Console.WriteLine("Found {0} values", info.Count);

      Console.ReadKey();
   }
}