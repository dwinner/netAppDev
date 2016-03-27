using System.Data.Entity;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Контект для таблиц профиля
   /// </summary>
   public class UsersContext : DbContext
   {
      public UsersContext()
         : base("DefaultConnection")
      {
      }

      /// <summary>
      /// Набор сущностей для профиля пользователя
      /// </summary>
      public DbSet<UserProfile> UserProfiles { get; set; }
   }
}