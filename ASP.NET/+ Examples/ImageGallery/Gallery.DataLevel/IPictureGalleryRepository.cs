using System;
using System.Linq;
using Gallery.DataLevel.Orm;

namespace Gallery.DataLevel
{
   /// <summary>
   /// Интерфейс для операций с сущностями PictureGallery
   /// </summary>
   public interface IPictureGalleryRepository : IDisposable
   {
      /// <summary>
      /// Получение сущностей для изображений
      /// </summary>
      IQueryable<PictureGallery> Pictures { get; }

      /// <summary>
      /// Сохранение сущности
      /// </summary>
      /// <param name="picture">Сущность для изображения</param>
      /// <param name="descriptionOnly">true, если нужно обновить только описание, false - в противном случае</param>      
      void Save(PictureGallery picture, bool descriptionOnly = false);

      /// <summary>
      /// Удаление сущности
      /// </summary>
      /// <param name="pictureId">Id для сущности изображения</param>
      /// <returns>Удаленную сущность изображения</returns>
      PictureGallery Delete(int pictureId);

      /// <summary>
      /// Получение значения аккаунта по имени
      /// </summary>
      /// <param name="loggedInName">Имя пользователя, указанное при входе</param>
      /// <returns>Значение аккаунта по имени</returns>
      int RetrieveAccountId(string loggedInName);
   }
}