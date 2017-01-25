using static System.Console;

namespace ImplicitTupleSample
{
   class Program
   {
      static void Main(string[] args)
      {
         (int, int) Tally()
         {
            return (0, 0);
         }

         var implTuple = Tally();
         WriteLine($"{implTuple.Item1}, {implTuple.Item2}");

         (int sum, int count) NamedTally()
         {
            return (0, 0);
         }

         var namedImplTuple = NamedTally();
         WriteLine($"Sum = {namedImplTuple.sum}, Count = {namedImplTuple.count}");
      }
   }
}
