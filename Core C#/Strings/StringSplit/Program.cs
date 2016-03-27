/**
 * Способы разбивки строки по разделителям (и без)
 */

using System;

namespace HowToCSharp.ch07.StringSplit
{
   class Program
   {
      static void Main()
      {
         const string original = "But, in a larger sense, we can not dedicate—we can not consecrate—we can not hallow—this ground.";
         char[] delims = new[] { ',', '-', ' ', '.' };
         string[] strings = original.Split(delims);
         Console.WriteLine("Default split behavior:");
         foreach (var s in strings)
         {
            Console.WriteLine("\t{0}", s);
         }
         Console.WriteLine();
         strings = original.Split(delims, StringSplitOptions.RemoveEmptyEntries);
         Console.WriteLine("StringSplitOptions.RemoveEmptyEntries:");
         foreach (var s in strings)
         {
            Console.WriteLine("\t{0}", s);
         }
         Console.ReadKey();
      }
   }
}
