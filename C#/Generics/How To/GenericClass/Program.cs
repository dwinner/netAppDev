/**
 * Параметризованный класс
 */

using System;

namespace HowToCSharp.ch09.GenericClass
{
   class Program
   {
      static void Main(string[] args)
      {
         Indexer<Part> indexer = new Indexer<Part>();
         Part p1 = new Part("1", "Part01", 1.5);
         Part p2 = new Part("2", "Part02", 2.0);

         indexer.Add(p1.PartId, p1);
         indexer.Add(p2.PartId, p2);

         Part p = indexer.Find("2");
         Console.WriteLine("Found: {0}", p);

         Console.ReadKey();
      }

      //class Temp<T, TS>
      //   where T : IComparable<T>, new()
      //   where TS : class, new() { }
   }
}
