using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _16_ContextPoolSample
{
   internal class Program
   {
      private ServiceProvider Container { get; set; }

      private static async Task Main()
      {
         var p = new Program();
         p.InitializeServices();
         p.ConfigureLogging();

         await using var container = p.Container;
         if (container == null)
         {
            return;
         }

         var service = container.GetService<BooksService>();
         Debug.Assert(service != null);

         await container.GetService<BooksController>().CreateDatabaseAsync().ConfigureAwait(false);
         await container.GetService<BooksController>().AddBooksAsync().ConfigureAwait(false);
         await container.GetService<BooksController>().ReadBooksAsync().ConfigureAwait(false);
         await container.GetService<BooksController>().ReadBooksAsync().ConfigureAwait(false);
         await container.GetService<BooksController>().ReadBooksAsync().ConfigureAwait(false);
         //await container.DisposeAsync().ConfigureAwait(false);
      }

      private void InitializeServices()
      {
         const string connectionString =
            @"server=(localdb)\MSSQLLocalDb;database=BooksWithContextPool;trusted_connection=true";
         var services = new ServiceCollection();
         services.AddTransient<BooksController>();
         services.AddTransient<BooksService>();
         services.AddEntityFrameworkSqlServer();
         services.AddDbContextPool<BooksContext>(options => options.UseSqlServer(connectionString));
         services.AddLogging();

         Container = services.BuildServiceProvider();
      }

      private void ConfigureLogging()
      {
         var loggerFactory = Container.GetService<ILoggerFactory>();
         loggerFactory?.AddProvider(new MyLoggerProvider());
      }
   }
}