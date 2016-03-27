/**
 * Постусловия
 */

using System;
using System.Diagnostics.Contracts;

namespace Postconditions
{
   [ContractOption(category: "runtime", setting: "checking", enabled: true)]
   class Program
   {
      private static int _sharedState = 5;

      static void Main()
      {
         Postcondition();
         Console.WriteLine(ReturnValue());
         Console.WriteLine(ReturnLargerThanInput(4));
         int x, y;
         OutParameters(out x, out y);
      }

      private static void Postcondition()
      {
         Contract.Ensures(_sharedState < 6);
         _sharedState = 9;
         Console.WriteLine("change sharedState invariant {0}", _sharedState);
         _sharedState = 3;
         Console.WriteLine("before returning change it to a valid value {0}", _sharedState);
      }

      private static int ReturnValue()
      {
         Contract.Ensures(Contract.Result<int>() < 6);
         return 3;
      }

      private static int ReturnLargerThanInput(int x)
      {
         Contract.Ensures(Contract.Result<int>() > Contract.OldValue<int>(x));
         return x + 3;
      }

      private static void OutParameters(out int x, out int y)
      {
         Contract.Ensures(Contract.ValueAtReturn<int>(out x) > 5 && Contract.ValueAtReturn<int>(out x) < 20);
         Contract.Ensures(Contract.ValueAtReturn<int>(out y) % 5 == 0);
         x = 8;
         y = 10;
      }
   }
}
