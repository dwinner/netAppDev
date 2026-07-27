using Microsoft.EntityFrameworkCore;

namespace Post.Query.Infrastructure.DataAccess;

public class DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
{
   public DatabaseContext CreateDbContext()
   {
      DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
      configureDbContext(optionsBuilder);

      return new DatabaseContext(optionsBuilder.Options);
   }
}