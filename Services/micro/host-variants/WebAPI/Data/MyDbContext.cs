namespace WebApi_Microservice.Data;

public class MyDbContext(DbContextOptions options) : DbContext(options)
{
   public DbSet<Order> Orders => Set<Order>();
}