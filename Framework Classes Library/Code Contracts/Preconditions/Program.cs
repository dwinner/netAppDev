/**
 * Предусловия
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Preconditions
{
   [ContractOption(category: "runtime", setting: "checking", enabled: true)]
   class Program
   {
      static void Main()
      {
         Contract.ContractFailed += (sender, args) => Console.WriteLine(args.Message)/*args.SetHandled();*/;
         MinMax(2, 7);

         int[] data = { 3, 5, 7, 11, 20 };
         ArrayTest(data);

         Preconditions(null);
      }

      private static void MinMax(int min, int max)
      {
         Contract.Requires(min <= max);
         Contract.Requires<ArgumentException>(min <= max);
      }

      private static void ArrayTest(IEnumerable<int> data)
      {
         Contract.Requires(Contract.ForAll(data, i => i < 12));
         Contract.Requires(Contract.Exists(data, i => i < 5));
         Console.WriteLine("ArrayTest contract succeeded");
      }

      private static void Preconditions(object o)
      {
         Contract.Requires<ArgumentNullException>(o != null, "Preconditions, o may not be null");
         Console.WriteLine(o.GetType().Name);
      }

      private static void ArrayTestWithPureMethod(IEnumerable<int> data)
      {
         Contract.Requires(Contract.ForAll(data, MyDataCheck));
      }

      public static int MaxVal { get; set; }

      public static bool MyDataCheck(int x)
      {
         return x <= MaxVal;
      }
   }
}
