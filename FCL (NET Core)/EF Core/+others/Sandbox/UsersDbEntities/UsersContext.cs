using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace UsersDbEntities
{
   public sealed class UsersContext : DbContext
   {
      private const string DefaultConnectionString =
         "Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;";

      private readonly StreamWriter _logStream = new StreamWriter("mylog.txt", true);

      public UsersContext()
      {
         //Database.EnsureDeleted();
         Database.EnsureCreated();
      }

      public UsersContext(DbContextOptions<UsersContext> options)
         : base(options)
      {
      }

      public DbSet<User> Users { get; set; }

      private static ILoggerFactory MyLoggerFactory =>
         LoggerFactory.Create(builder => builder
            .AddConsole()
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name)
            .AddProvider(new MyLoggerProvider()));

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer(DefaultConnectionString);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.LogTo(
               message => Debug.WriteLine(message),
               new[] {RelationalEventId.CommandExecuted} // logging categories
            );
         }
      }

      public override void Dispose()
      {
         base.Dispose();
         _logStream.Dispose();
      }

      public override async ValueTask DisposeAsync()
      {
         await base.DisposeAsync().ConfigureAwait(false);
         await _logStream.DisposeAsync().ConfigureAwait(false);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
         // OnModelCreatingPartial(modelBuilder);
      }

      //private static void OnModelCreatingPartial(ModelBuilder modelBuilder)
      //{
      //}
   }
}