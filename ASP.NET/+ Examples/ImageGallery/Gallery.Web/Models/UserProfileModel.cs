using System.ComponentModel.DataAnnotations;
using Gallery.Web.Properties;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель для профиля пользователя
   /// </summary>
   public class UserProfileModel
   {
      /// <summary>
      /// Имя
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

      /// <summary>
      /// Количество сущностей на одной странице
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "PageSize")]
      [Required]
      [Range(5, 50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "PageSizeMustBeIn")]
      public int PageSize { get; set; }

      /// <summary>
      /// Максимальный размер изображения
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "MaxPictureDim")]
      [Required]
      [Range(50, 300, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MaxPictureMeasurementError")]
      public int MaxPictureMeasurement { get; set; }
   }
}