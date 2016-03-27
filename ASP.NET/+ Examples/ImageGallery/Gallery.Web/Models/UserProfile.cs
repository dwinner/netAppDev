using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Web.Models
{
   /// <summary>
   /// POCO-класс для таблицы профиля входа пользователя
   /// </summary>
   [Table("UserProfile")]
   public class UserProfile
   {
      /// <summary>
      /// Id пользователя
      /// </summary>
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int UserId { get; set; }

      /// <summary>
      /// Имя пользователя, указанное при входе
      /// </summary>
      public string UserName { get; set; }
   }
}