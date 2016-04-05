/**
 * Корректное сравнение строк
 */

using System;
using System.Globalization;

namespace HowToCSharp.ch07.StringCompare
{
   class Program
   {
      static void Main(string[] args)
      {
         const string a = "file";
         const string b = "FILE";
         bool equalInvariant = string.Compare(a, b, true, CultureInfo.InvariantCulture) == 0;
         bool equalTurkish = string.Compare(a, b, true, CultureInfo.CreateSpecificCulture("tr-TR")) == 0;

         Console.WriteLine("Are {0} and {1} equal?", a, b);
         Console.WriteLine("Invariant culture: {0}", (equalInvariant ? "yes" : "no"));
         Console.WriteLine("Turkish culture: {0}", (equalTurkish ? "yes" : "no"));

         Console.ReadKey();
      }
   }
}
