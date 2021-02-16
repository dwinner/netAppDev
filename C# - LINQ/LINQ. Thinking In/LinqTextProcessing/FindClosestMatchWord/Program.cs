// Нахождение "ближайших" совпадений

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FindClosestMatchWord
{
   internal static class Program
   {
      private const string T9SuggestionFile = "T9.txt";

      private static void Main()
      {
         string total;
         using (var reader = new StreamReader(T9SuggestionFile))
         {
            total = reader.ReadToEnd();
         }

         var query = total.Split(new[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
         const string path = "ujnbvcderesdftrewazxcvbnhgfds";
         var result =
            query.Where(word => path.LongestCommonSubsequence(word).Equals(word))
               .OrderByDescending(word => word.Length)
               .ThenByDescending(word => word)
               .Take(4);
         foreach (var s in result)
         {
            Console.WriteLine(s);
         }
      }
   }

   public static class StringEx
   {
      private static int[,] CreateLcsMatrix(string x, string y)
      {
         var m = x.Length;
         var n = y.Length;
         var t = new int[m, n];
         for (var i = 0; i < m; i++)
         {
            t[i, 0] = 0;
         }
         for (var i = 0; i < n; i++)
         {
            t[0, i] = 0;
         }
         for (var i = 1; i < m; i++)
         {
            for (var j = 1; j < n; j++)
            {
               t[i, j] = x[i] == y[j] ? t[i - 1, j - 1] + 1 : Math.Max(t[i, j - 1], t[i - 1, j]);
            }
         }

         return t;
      }

      public static string LongestCommonSubsequence(this string x, string y)
      {
         var lcs = new List<char>();
         var sb = new StringBuilder();

         x = string.Format(" {0}", x);
         y = string.Format(" {0}", y);
         var t = CreateLcsMatrix(x, y);

         var m = x.Length;
         var n = y.Length;
         var i = m - 1;
         var j = n - 1;

         while (i > 0 && j > 0)
         {
            if (t[i, j] == t[i - 1, j - 1] + 1 && x[i] == y[j])
            {
               lcs.Add(x[i]);
               i--;
               j--;
            }
            else if (t[i - 1, j] > t[i, j - 1])
            {
               i--;
            }
            else
            {
               j--;
            }
         }

         for (var p = lcs.Count - 1; p >= 0; p--)
         {
            sb.Append(lcs[p].ToString());
         }

         return sb.ToString();
      }
   }
}