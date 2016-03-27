using System.Data.Entity.Migrations;
using System.Linq;
using IdentityAndSecurity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityAndSecurity.Migrations
{
   internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
   {
      public Configuration()
      {
         AutomaticMigrationsEnabled = true;
         ContextKey = "IdentityAndSecurity.Models.ApplicationDbContext";
      }

      protected override void Seed(ApplicationDbContext context)
      {
         #region Comments

         //  This method will be called after migrating to the latest version.

         //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
         //  to avoid creating duplicate seed data. E.g.
         //
         //    context.People.AddOrUpdate(
         //      p => p.FullName,
         //      new Person { FullName = "Andrew Peters" },
         //      new Person { FullName = "Brice Lambson" },
         //      new Person { FullName = "Rowan Miller" }
         //    );
         //

         //var hasher = new PasswordHasher();
         //context.Users.AddOrUpdate(user => user.UserName,
         //   new ApplicationUser
         //   {
         //      UserName = "dwinner",
         //      PasswordHash = hasher.HashPassword("password")
         //   });

         #endregion

         if (!context.Users.Any(user => user.UserName == "dwinner"))
         {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser { UserName = "dwinner" };
            manager.Create(user, "password");
         }
      }
   }
}