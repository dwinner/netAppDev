using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Gallery.DataLevel.Properties;

namespace Gallery.DataLevel.Orm.Extensions
{
   /// <summary>
   /// Метаданные для таблицы Picture
   /// </summary>
   public class PictureGallaryMetadata
   {
      /// <summary>
      /// Id-фотографии
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public int PictureId { get; set; }

      /// <summary>
      /// Id-аккаунта
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public int AccountId { get; set; }

      /// <summary>
      /// Ширина фотографии
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public int Width { get; set; }

      /// <summary>
      /// Высота фотографии
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public int Height { get; set; }

      /// <summary>
      /// Ширина фотографии
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public string PictureFileName { get; set; }

      /// <summary>
      /// Данные фотографии
      /// </summary>
      public byte[] PictureData { get; set; }

      /// <summary>
      /// Описание фотографии
      /// </summary>
      [Display(ResourceType = typeof(Resources), Name = "Description")]
      [DataType(DataType.Text)]
      [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DescriptionRequired")]
      public string PictureDescription { get; set; }

      /// <summary>
      /// MimeType изображения
      /// </summary>
      [HiddenInput(DisplayValue = false)]
      public string PictureMimeType { get; set; }
   }
}