using System.Data.Entity;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public sealed class SqLiteDatabaseContext : DbContext
	{
		public SqLiteDatabaseContext() : base("DefaultConnection")
		{
		}

		public DbSet<Phone> Phones { get; set; }
	}
}