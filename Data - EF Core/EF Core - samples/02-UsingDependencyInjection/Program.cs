/*
 * Using dependency injection
 */

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _02_UsingDependencyInjection
{
   internal class Program
   {
      public ServiceProvider Container { get; private set; }

      private static async Task Main()
      {
         var p = new Program();
         p.InitializeServices();
         var service = p.Container.GetService<BooksService>();
         await service.AddBooksAsync().ConfigureAwait(false);
         await service.ReadBooksAsync().ConfigureAwait(false);
         await p.Container.DisposeAsync().ConfigureAwait(false);
      }

      private void InitializeServices()
      {
         const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
         var services = new ServiceCollection();
         services.AddTransient<BooksService>()
            .AddEntityFrameworkSqlServer()
            .AddDbContext<BooksContext>(builder => builder.UseSqlServer(connectionString));
         services.AddLogging();

         Container = services.BuildServiceProvider();
      }

      /*private void ConfigureLogging()
      {
         var loggerFactory = Container.GetService<ILoggerFactory>();
         loggerFactory.AddConsole(LogLevel.Information);
      }*/
   }
}