/**
 * Facade как интерфейс к подсистеме
 */

namespace Facade
{
   internal static class Program
   {
      private static void Main()
      {
         var gopher = new DealerRepresentative();
         gopher.GetPizzaUpdate();
      }
   }
}