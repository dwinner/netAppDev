/**
 * Именованные группы в совпадениях
 */

using System;
using System.Text.RegularExpressions;

namespace NamingGroupsInMatch
{
   class Program
   {
      private const string IpAddressRegExpr = @"(?<part1>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part2>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part3>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part4>[01]?\d\d?|2[0-4]\d|25[0-5])";

      private const string TestString = "Что-то там, что-то там еще. 127.0.0.1 Что-то там еще... 999.999.999.99" +
                                        "- это уже неправильный IP-адрес. И этот 245.67.304.102 тоже неправильный." +
                                        "Вот еще один правильный: 192.168.7.7";

      static void Main()
      {
         var ipAddressRegex = new Regex(IpAddressRegExpr);
         Match match = ipAddressRegex.Match(TestString);
         while (match.Success)
         {
            Console.WriteLine("IP-адрес найден в позиции {0} со значением {1}",
               match.Index,
               match.Value);
            Console.WriteLine("Группы:");
            Console.WriteLine("\tЧасть 1: {0}", match.Groups["part1"]);
            Console.WriteLine("\tЧасть 2: {0}", match.Groups["part2"]);
            Console.WriteLine("\tЧасть 3: {0}", match.Groups["part3"]);
            Console.WriteLine("\tЧасть 4: {0}", match.Groups["part4"]);
            match = match.NextMatch();
         }

         Console.ReadKey();
      }
   }
}
