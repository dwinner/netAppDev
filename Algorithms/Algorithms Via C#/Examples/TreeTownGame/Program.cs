using System;
using System.Linq;

namespace TreeTownGame
{
   internal static class Program
   {
      private const string DefaultPath = "Towns.txt";

      private static void Main(string[] args)
      {
         string tonwsFileName = args.Length == 1 ? args[0] : DefaultPath;
         string[] testTowns = TownsReader.ReadTowns(tonwsFileName).ToArray();
         var townsTree = new WordTree(testTowns);

         foreach (string chainVariant in townsTree.UniqueChains)         
            Console.WriteLine("Max chain variant: {0}", chainVariant);         
      }
   }
}