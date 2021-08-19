/**
 * Замена текста с помощью регулярных выражений
 */

using System;
using System.Text.RegularExpressions;

namespace RegexReplace
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
         var regex = new Regex(IpAddressRegExpr);
         Console.WriteLine("Полученный ввод -> {0}",
            regex.Replace(TestString, "xxx.xxx.xxx.xxx"));
      }
   }
}
