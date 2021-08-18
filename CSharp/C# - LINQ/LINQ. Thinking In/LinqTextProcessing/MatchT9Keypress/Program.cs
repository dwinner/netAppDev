// T9-эмуляция

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MatchT9Keypress
{
   internal static class Program
   {
      private const string T9FileName = "T9.txt";

      private static void Main()
      {
         const string keyPad = @"2 = ABC2
3 = DEF3
4 = GHI4
5 = JKL5
6 = MNO6
7 = PQRS7
8 = TUV8
9 = WXYZ9";

         var keyMap = new Dictionary<char, char>();

         // сочетание 4663 может быть слово "home", "good" etc
         const string key = "4663";

         var keysAndLetters = keyPad.ToLower().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => new KeyValuePair<string, string>(p.Split('=')[0].Trim(), p.Split('=')[1].Trim())).ToList();

         foreach (var keyL in keysAndLetters)
         {
            foreach (var @char in keyL.Value.ToCharArray())
            {
               keyMap.Add(@char, Convert.ToChar(keyL.Key));
            }
         }

         string total;
         using (var reader = new StreamReader(T9FileName))
         {
            total = reader.ReadToEnd();
            reader.Close();
         }

         var query =
            total.Split(new[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
               .Where(t => t.Length == key.Length)
               .Select(t => t.Trim());
         var dump =
            query.ToList()
               .Select(
                  w => new KeyValuePair<string, string>(w, new string(w.ToCharArray().Select(x => keyMap[x]).ToArray())))
               .Where(w => w.Value == key)
               .Select(w => w.Key);

         foreach (var s in dump)
         {
            Console.WriteLine(s);
         }
      }
   }
}