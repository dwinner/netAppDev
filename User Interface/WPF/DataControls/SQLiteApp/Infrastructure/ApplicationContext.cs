using System.Data.Entity;
using SQLiteApp.Models;

namespace SQLiteApp.Infrastructure
{
	public sealed class ApplicationContext : DbContext
	{
		public ApplicationContext() : base("DefaultConnection")
		{
		}

		public DbSet<Phone> Phones { get; set; }
	}
}