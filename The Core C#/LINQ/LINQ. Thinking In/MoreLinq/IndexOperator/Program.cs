// Индексирование произвольных последовательностей

using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace IndexOperator
{
   internal static class Program
   {
      private static void Main()
      {
         // Первый вариант
         const string input = "LINQ";
         input.Select((@char, index) => new KeyValuePair<int, char>(index, @char))
            .ForEach(pair => Console.WriteLine("{0}: {1}", pair.Key, pair.Value));

         // Второй вариант
         "LINQ".ToCharArray().Index().ForEach(pair => Console.WriteLine("{0}: {1}", pair.Key, pair.Value));
      }
   }
}