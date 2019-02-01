/**
 * Обобщенные методы
 */

using System;
using System.Collections.Generic;

namespace GenericMethods
{
   static class Program
   {
      static void Main()
      {
         IList<IAccount> accounts = new List<IAccount>
         {
            new Account("Christian", 1500),
            new Account("Stephanie", 2200),
            new Account("Angela", 1800),
            new Account("Matthias", 2400)
         };

         decimal amount = Algorithm.Accumulate(accounts);
         Console.WriteLine(amount);

         amount = Algorithm.Accumulate<IAccount>(accounts);
         Console.WriteLine(amount);

         amount = Algorithm.Accumulate<IAccount, decimal>(accounts, (item, sum) => sum + item.Balance);
         Console.WriteLine(amount);

         Console.ReadKey();
      }
   }
}
