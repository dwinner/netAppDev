/**
 * Базовый поиск с использованием регулярных выражений
 */

using System;
using System.Text.RegularExpressions;

namespace BaseSearch
{
   class Program
   {
      private const string SimpleIpAddressRegExpr = @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?";
      private const string TestString = "Что-то там, что-то там еще. 127.0.0.1 Что-то там еще...";

      static void Main()
      {
         // Создать регулярное выражение для поиска шаблона IP-адреса
         var ipAddressRegex = new Regex(SimpleIpAddressRegExpr);
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
