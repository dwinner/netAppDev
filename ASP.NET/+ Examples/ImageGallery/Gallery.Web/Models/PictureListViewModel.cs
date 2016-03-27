using System.Collections.Generic;
using Gallery.DataLevel.Orm;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель для представления списка изображений
   /// </summary>
   public class PictureListViewModel
   {
      /// <summary>
      /// Изображения
      /// </summary>
      public IEnumerable<PictureGallery> Pictures { get; set; }

      /// <summary>
      /// Страничный навигатор
      /// </summary>
      public PagingNavigator PageNavigator { get; set; }
   }
}