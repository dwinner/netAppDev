/**
 * Facade как интерфейс к подсистеме
 */

namespace Facade
{
   internal static class Program
   {
      static void Main()
      {
         var gopher = new DealerRepresentative();
         gopher.GetPizzaUpdate();
      }
   }
}
