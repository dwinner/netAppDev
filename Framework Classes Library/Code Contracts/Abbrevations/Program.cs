/**
 * Сокращения для контрактов
 */

using System;
using System.Diagnostics.Contracts;

namespace Abbrevations
{
   static class Program
   {
      static void Main()
      {
         int[] data = { 3, 5, 7, 11, 20 };
         Abbrevations(data);
      }

      [ContractAbbreviator]
      private static void CheckCollectionContract(int[] data)
      {
         Contract.Requires<ArgumentNullException>(data != null);
         Contract.Requires(Contract.ForAll(data, x => x < 12));
      }

      private static void Abbrevations(int[] data)
      {
         CheckCollectionContract(data);
      }
   }
}
