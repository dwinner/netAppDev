using System.ComponentModel.DataAnnotations;
using Gallery.Web.Properties;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель регистрации для внешних служб
   /// </summary>
   public class RegisterExternalLoginModel
   {
      /// <summary>
      /// Имя пользователя
      /// </summary>
      [Required]
      [Display(ResourceType = typeof(Resources), Name = "UserNameMessage")]
      public string UserName { get; set; }

      /// <summary>
      /// Данные для внешних служб для входа
      /// </summary>
      public string ExternalLoginData { get; set; }
   }
}