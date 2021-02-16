using DataMigration.Business;
using Ninject;

namespace DataMigration.Console
{
   internal static class Program
   {
      private static void Main()
      {
         var kernel = new StandardKernel(new CompositionModule());
         var shippersService = kernel.Get<ShippersService>();
         shippersService.MigrateShippers();
      }
   }
}