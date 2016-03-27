using System.ComponentModel.DataAnnotations;
using Gallery.Web.Properties;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель регистрации
   /// </summary>
   public class RegisterModel
   {
      /// <summary>
      /// Имя пользователя при входе
      /// </summary>
      [Required]
      [Display(ResourceType = typeof(Resources), Name = "UserNameMessage")]
      public string UserName { get; set; }

      /// <summary>
      /// Пароль пользователя
      /// </summary>
      [Required]
      [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValueMustGreaterOrEqualTwo", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "Password")]
      public string Password { get; set; }

      /// <summary>
      /// Подтверждение пароля
      /// </summary>
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "ConfirmPassword")]
      [Compare("Password", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "PasswordAndHisConfirmedAreNotTheSame")]
      public string ConfirmPassword { get; set; }

      /// <summary>
      /// Имя пользователя
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "FirstName")]
      public string FirstName { get; set; }

      /// <summary>
      /// Отчество
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "PatronymicName")]
      public string PatronymicName { get; set; }

      /// <summary>
      /// Фамилия
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "LastName")]
      public string LastName { get; set; }
   }
}