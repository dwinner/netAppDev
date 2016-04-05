/*
 * Контракты кода.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CodeContracts
{
   class Program
   {
      [ContractClass(typeof(AddContract))]
      interface IAdd
      {
         uint Add(uint a, uint b);
      }

      [ContractClassFor(typeof(IAdd))]
      class AddContract : IAdd
      {
         // Закрытая, явная реализация интерфейса
         uint IAdd.Add(uint a, uint b)
         {
            Contract.Requires((ulong)a + (ulong)b < uint.MaxValue);
            return a + b;
         }
      }

      // Этот класс не нуждается в спецификации контракта
      class BetterAdd : IAdd
      {
         public uint Add(uint a, uint b)
         {
            return a + b;
         }
      }

      static void Main(string[] args)
      {
         var list = new List<int>();
         AppendNumber(list, 13);

         AppendNumber(list, -1);

         var ba = new BetterAdd();
         uint result = ba.Add(uint.MaxValue, uint.MaxValue);

         Console.ReadKey();
      }

      static void AppendNumber(List<int> list, int newNumber)
      {
         Contract.Requires(newNumber > 0, "Failed contract: negative");
         Contract.Ensures(list.Count == Contract.OldValue(list.Count) + 1);

         list.Add(newNumber);
      }
   }
}
