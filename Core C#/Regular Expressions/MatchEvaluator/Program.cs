/**
 * Замена совпадений обратным вызовом
 */

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MatchEvaluator
{
   class Program
   {
      private const string IpAddressRegExpr = @"(?<part1>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part2>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part3>[01]?\d\d?|2[0-4]\d|25[0-5])\." +
                                              @"(?<part4>[01]?\d\d?|2[0-4]\d|25[0-5])";

      private const string TestString = "Что-то там, что-то там еще. 127.0.0.1 Что-то там еще... 999.999.999.99" +
                                        "- это уже неправильный IP-адрес. И этот 245.67.304.102 тоже неправильный." +
                                        "Вот еще один правильный: 192.168.7.12";

      static void Main()
      {
         var regex = new Regex(IpAddressRegExpr);

         // Использование делегата при обнаружении совпадения

         Console.WriteLine(regex.Replace(TestString, aMatch =>
         {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}.", aMatch.Groups["part4"])
                         .AppendFormat("{0}.", aMatch.Groups["part3"])
                         .AppendFormat("{0}.", aMatch.Groups["part2"])
                         .Append(aMatch.Groups["part1"]);
            return stringBuilder.ToString();
         }));

         // Использование подстановки с помощью регулярных выражений

         const string replace = @"${part4}.${part3}.${part2}.${part1}" +
                                @" (the reverse of $&)";
         Console.WriteLine(regex.Replace(TestString, replace));

         Console.ReadKey();
      }
   }
}
