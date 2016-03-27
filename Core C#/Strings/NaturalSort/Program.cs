/**
 * Естественная сортировка строк
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HowToCSharp.ch07.NaturalSort
{
   class Program
   {
      static void Main()
      {
         string[] originals = new[]
            {
                "Part 1", "Part 2", "Part 3", "Part 4", "Part 5",
                "Part 6", "Part 7", "Part 8", "Part 9", "Part 10",
                "Part 11", "Part 12", "Part 13", "Part 14", "Part 15",
                "Part 16", "Part 17", "Part 18", "Part 19", "Part 20"
            };

         // Обычная сортировка
         Console.WriteLine("Naive sort:");
         List<string> copy = new List<string>(originals);
         copy.Sort((x, y) => String.Compare(x, y, StringComparison.Ordinal));
         foreach (string s in copy)
         {
            Console.WriteLine("\t{0}", s);
         }

         Console.WriteLine();
         // Естественная сортировка
         Console.WriteLine("Natural Sort:");
         copy = new List<string>(originals);
         copy.Sort(new NaturalSorter());
         foreach (string s in copy)
         {
            Console.WriteLine("\t{0}", s);
         }

         Console.ReadKey();
      }
   }

   /// <summary>
   /// Сортировщик для естественной упорядоченности элементов
   /// </summary>
   class NaturalSorter : IComparer<string>
   {
      // для повышения производительности используется буфер,
      // потому что мы собираемся многократно вызывать метод Compare
      private const int DefaultSplitBufferSize = 256;
      private readonly char[] _splitBuffer;
      private readonly int _splitBufferSize;

      public NaturalSorter()
         : this(DefaultSplitBufferSize)
      { }

      public NaturalSorter(int splitBufferSize)
      {
         _splitBufferSize = splitBufferSize;
         _splitBuffer = new char[_splitBufferSize];
      }

      public int Compare(string x, string y)
      {
         // Вначале разобьем строку на сегменты, содержащие
         // цифры и нецифровые символы
         IList<string> a = SplitByNumbers(x);
         IList<string> b = SplitByNumbers(y);

         int aInt, bInt;
         int numToCompare = (a.Count < b.Count) ? a.Count : b.Count;
         for (int i = 0; i < numToCompare; i++)
         {
            if (a[i].Equals(b[i]))
               continue;

            bool aIsNumber = Int32.TryParse(a[i], out aInt);
            bool bIsNumber = Int32.TryParse(b[i], out bInt);
            bool bothNumbers = aIsNumber && bIsNumber;
            bool bothNotNumbers = !aIsNumber && !bIsNumber;
            if (bothNumbers)  // Выполняем сравнение целых чисел
               return aInt.CompareTo(bInt);
            if (bothNotNumbers)  // Выполняем сравнение строк
               return String.Compare(a[i], b[i], StringComparison.Ordinal);
            // Только один из двух сегментов является числом;
            // по определению числа предшествуют нецифровым подстрокам
            return aIsNumber ? -1 : 1;
         }
         // Сюда мы попадаем, только если одна из строк пуста
         return a.Count.CompareTo(b.Count);
      }

      private IList<string> SplitByNumbers(string val)
      {
         Debug.Assert(val.Length <= _splitBufferSize);
         List<string> list = new List<string>();
         int current = 0;
         int dest = 0;
         while (current < val.Length)
         {
            while (current < val.Length && !char.IsDigit(val[current]))	// Накапливаем нечисловые подстроки
            {
               _splitBuffer[dest++] = val[current++];
            }
            if (dest > 0)
            {
               list.Add(new string(_splitBuffer, 0, dest));
               dest = 0;
            }
            while (current < val.Length && char.IsDigit(val[current]))	// Накапливаем подстроки с числами
            {
               _splitBuffer[dest++] = val[current++];
            }
            if (dest > 0)
            {
               list.Add(new string(_splitBuffer, 0, dest));
               dest = 0;
            }
         }

         return list;
      }
   }
}
