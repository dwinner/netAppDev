/**
 * Обратные ссылки в совпадениях
 */

using System;
using System.Text.RegularExpressions;

namespace BackLinks
{
   class Program
   {
      private const string IpAddressRegExpr = @"(?<part1>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"\k<part1>\." +
                                              @"\k<part1>\." +
                                              @"\k<part1>";

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
            match = match.NextMatch();
         }

         Console.ReadKey();
      }
   }
}
