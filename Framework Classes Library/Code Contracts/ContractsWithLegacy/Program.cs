/**
 * Контракты и унаследованный код
 */

using System;
using System.Diagnostics.Contracts;

namespace ContractsWithLegacy
{
   class Program
   {
      static void Main()
      {
         PreconditionsWithLegacyCode(new object());
      }

      static void PreconditionsWithLegacyCode(object o)
      {
         if (o == null)
            throw new ArgumentException("o");
         Contract.EndContractBlock();
      }
   }
}
