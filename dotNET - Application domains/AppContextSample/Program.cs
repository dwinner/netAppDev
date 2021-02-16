using System;

namespace AppContextSample
{
   internal static class Program
   {
      private const string BreakingChangeSwitchLabel = "SomeBreakingChange";

      private static void Main()
      {
         AppContext.SetSwitch(BreakingChangeSwitchLabel, true);
         if (AppContext.TryGetSwitch(BreakingChangeSwitchLabel, out var switchValue))
            Console.WriteLine(switchValue);
      }
   }
}