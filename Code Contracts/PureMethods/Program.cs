/**
 * Требование неизменности
 */

using System;
using System.Diagnostics.Contracts;

namespace PureMethods
{
   //[Pure]
   class Program
   {
      public string SomeData { get; set; }

      static void Main()
      {
         var program = new Program { SomeData = "init value" };
         program.PureMethod();
      }

      [Pure]
      void PureMethod()
      {
         SomeData = "New value";
         Console.WriteLine(SomeData);
      }
   }
}
