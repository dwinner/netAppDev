using System.ComponentModel.DataAnnotations;
using Gallery.Web.Properties;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель для входа
   /// </summary>
   public class LoginModel
   {
      /// <summary>
      /// Имя пользователя
      /// </summary>
      [Required]
      [Display(ResourceType = typeof(Resources), Name = "UserNameMessage")]
      public string UserName { get; set; }

      /// <summary>
      /// Пароль
      /// </summary>
      [Required]
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "Password")]
      public string Password { get; set; }

      /// <summary>
      /// Флаг для сохранения учетных данных при входе
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "RememberMe")]
      public bool RememberMe { get; set; }
   }
}