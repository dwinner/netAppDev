using static System.Console;

namespace ImplicitTupleSample
{
   internal static class Program
   {
      private static void Main()
      {
         (int, int) Tally() => (0, 0);

         var (item1, item2) = Tally();
         WriteLine($"{item1}, {item2}");

         (int sum, int count) NamedTally() => (0, 0);

         var (sum, count) = NamedTally();
         WriteLine($"Sum = {sum}, Count = {count}");
      }
   }
}