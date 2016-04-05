using System.ComponentModel.DataAnnotations;
using Gallery.Web.Properties;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель для паролей для локального входа
   /// </summary>
   public class LocalPasswordModel
   {
      /// <summary>
      /// Старый пароль
      /// </summary>
      [Required]
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "CurrentPasswordMessage")]
      public string OldPassword { get; set; }

      /// <summary>
      /// Новый пароль
      /// </summary>
      [Required]
      [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValueMustGreaterOrEqualTwo", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "NewPassword")]
      public string NewPassword { get; set; }

      /// <summary>
      /// Подтверждение пароля
      /// </summary>
      [DataType(DataType.Password)]
      [Display(ResourceType = typeof(Resources), Name = "ConfirmPassword")]
      [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "PasswordAndHisConfirmedAreNotTheSame")]
      public string ConfirmPassword { get; set; }
   }
}