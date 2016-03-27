/**
 * Пример, демонстрирующий делегаты в виде указателей на методы
 */

using System;

namespace GetaStringDemo
{
   static class Program
   {
      private delegate string GetaString();

      static void Main()
      {
         int x = 40;
         GetaString firstStringMethod = x.ToString;
         Console.WriteLine("String is {0}", firstStringMethod());

         var balance = new Currency(34, 50);

         // firstStringMethod ссылается на метод экземпляра
         firstStringMethod = balance.ToString;
         Console.WriteLine("String is {0}", firstStringMethod());

         // firstStringMethod ссылается на статический метод
         firstStringMethod = Currency.GetCurrencyUnit;
         Console.WriteLine("String is {0}", firstStringMethod());

         Console.ReadLine();
      }
   }
}
