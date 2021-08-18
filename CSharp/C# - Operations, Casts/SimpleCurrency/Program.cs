/**
 * Пользовательские операции преобразования
 */

using System;

namespace SimpleCurrency
{
   static class Program
   {
      static void Main()
      {
         try
         {
            var balance = new Currency(50, 35);

            Console.WriteLine(balance);
            Console.WriteLine("Balance is: {0}", balance);
            Console.WriteLine("Balance is (using ToString()){0}", balance);

            float balance2 = balance;

            Console.WriteLine("After converting to float, = {0}", balance2);

            balance = (Currency)balance2;

            Console.WriteLine("After converting back to Currency, = {0}", balance);
            Console.WriteLine("Now attempt to convert out of range value of -$50.50 to a Currency:");

            balance = (Currency)(-50.00);
            Console.WriteLine("Result is {0}", balance);
         }
         catch (OverflowException overflowEx)
         {
            Console.WriteLine(overflowEx);
         }
      }
   }
}
