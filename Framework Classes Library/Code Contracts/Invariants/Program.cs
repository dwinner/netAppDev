/**
 * Инварианты
 */

using System;

namespace Invariants
{

   partial class Program
   {
      private int _x = 10;

      static void Main()
      {
         var program = new Program();
         program.Invariant();
      }

      public void Invariant()
      {
         _x = 3;
         Console.WriteLine("invariant value: {0}", _x);
         // Нарушение контракта в конце метода
      }
   }
}
