using System;

namespace Gallery.Web.Models
{
   /// <summary>
   /// Модель постраничного навигатора
   /// </summary>
   public class PagingNavigator
   {
      /// <summary>
      /// Общее количество сущностей
      /// </summary>
      public int TotalItems { get; set; }

      /// <summary>
      /// Количество сущностей на одной странице
      /// </summary>
      public int ItemsPerPage { get; set; }

      /// <summary>
      /// Текущий номер страницы
      /// </summary>
      public int CurrentPage { get; set; }

      /// <summary>
      /// Общее количество страниц
      /// </summary>
      public int TotalPages
      {
         get { return (int)Math.Ceiling((decimal)TotalItems / (ItemsPerPage <= 0 ? 1 : ItemsPerPage)); }
      }
   }
}