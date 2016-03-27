// Форматирование на лету

using System;
using System.Collections.Generic;
using System.Linq;

namespace FormattingOnTheFly
{
   internal static class Program
   {
      private static void Main()
      {
         string[] someVals = { "234567890", "345678901", "456789012" };
         var modifiedVals = new List<string>();
         var transformer = FormatLikeThis("123456789=>123-456-789");
         someVals.ToList().ForEach(k => modifiedVals.Add(transformer(k)));

         var zippedTransformer = someVals.Zip(modifiedVals, (a, b) => string.Format("{0} -> {1}", a, b));
         foreach (var word in zippedTransformer)
         {
            Console.WriteLine(word);
         }
      }

      private static Func<string, string> FormatLikeThis(string transformation)
      {
         var tokens = transformation.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
         var start = tokens[0];
         var end = tokens[1];
         var insertCharMap = new Dictionary<int, char>();
         Enumerable.Range(0, end.Length)
            .Where(k => !start.Contains(end[k]))
            .ToList()
            .ForEach(k => insertCharMap.Add(k, end[k]));
         Func<string, string> transformer = x =>
         {
            insertCharMap.ToList().ForEach(z => x = x.Insert(z.Key, z.Value.ToString()));
            return x;
         };

         return transformer;
      }
   }
}