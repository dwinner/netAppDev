using System;
using System.Data.Entity;
using System.Linq;
using Gallery.DataLevel.Orm;

namespace Gallery.DataLevel
{
   /// <summary>
   /// Класс для операций с сущностями PictureGallery через ADO.NET EF 5
   /// </summary>
   public class PictureGalleryRepositoryImpl : IPictureGalleryRepository
   {
      private readonly ImageGalleryEntities _galleryEntities = new ImageGalleryEntities();

      /// <summary>
      /// Получение сущностей для изображений
      /// </summary>
      public IQueryable<PictureGallery> Pictures
      {
         get
         {
            return _galleryEntities.PictureGalleries.Include(gallery => gallery.PictureDetail);
         }
      }

      /// <summary>
      /// Сохранение сущности
      /// </summary>
      /// <param name="picture">Сущность для изображения</param>
      /// <param name="descriptionOnly">true, если нужно обновить только описание, false - в противном случае</param>    
      public void Save(PictureGallery picture, bool descriptionOnly = false)
      {
         if (descriptionOnly)
         {
            var pic = _galleryEntities.PictureGalleries.Find(picture.PictureId);
            if (pic != null)
            {
               pic.PictureDescription = picture.PictureDescription ?? string.Empty;
            }
         }
         else
         {
            if (picture.PictureId == 0)
            {
               _galleryEntities.PictureGalleries.Add(picture);
            }
            else
            {
               PictureGallery pictureGallery = _galleryEntities.PictureGalleries.Find(picture.PictureId);
               if (pictureGallery != null)
               {
                  pictureGallery.PictureDescription = picture.PictureDescription ?? string.Empty;
                  pictureGallery.Width = picture.Width;
                  pictureGallery.Height = picture.Height;
                  pictureGallery.PictureFileName = picture.PictureFileName;
                  pictureGallery.PictureData = picture.PictureData;
                  pictureGallery.PictureMimeType = picture.PictureMimeType;

                  pictureGallery.PictureDetail.CameraManufacturer = picture.PictureDetail.CameraManufacturer;
                  pictureGallery.PictureDetail.CameraModel = picture.PictureDetail.CameraModel;
                  pictureGallery.PictureDetail.CompressedBitsPerPixel = picture.PictureDetail.CompressedBitsPerPixel;
                  pictureGallery.PictureDetail.ExposureTime = picture.PictureDetail.ExposureTime;
                  pictureGallery.PictureDetail.FNumber = picture.PictureDetail.FNumber;
                  pictureGallery.PictureDetail.FocalLength = picture.PictureDetail.FocalLength;
                  pictureGallery.PictureDetail.ShootingDate = picture.PictureDetail.ShootingDate;
                  pictureGallery.PictureDetail.ShutterSpeedValue = picture.PictureDetail.ShutterSpeedValue;
               }
            }
         }

         _galleryEntities.SaveChanges();
      }

      /// <summary>
      /// Удаление сущности
      /// </summary>
      /// <param name="pictureId">Id для сущности изображения</param>
      /// <returns>Удаленную сущность изображения</returns>
      public PictureGallery Delete(int pictureId)
      {
         PictureGallery pictureGallery = _galleryEntities.PictureGalleries.Find(pictureId);
         if (pictureGallery != null)
         {
            _galleryEntities.PictureGalleries.Remove(pictureGallery);
            _galleryEntities.SaveChanges();
         }

         return pictureGallery;
      }

      /// <summary>
      /// Получение значения аккаунта по имени
      /// </summary>
      /// <param name="loggedInName">Имя пользователя, указанное при входе</param>
      /// <returns>Значение аккаунта по имени</returns>
      public int RetrieveAccountId(string loggedInName)
      {
         return (from account in _galleryEntities.Accounts
                 where account.UserName == loggedInName
                 select account.AccountId).SingleOrDefault();
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~PictureGalleryRepositoryImpl()
      {
         Dispose(false);
      }

      protected virtual void Dispose(bool disposing)
      {
         if (disposing && _galleryEntities != null)
         {
            _galleryEntities.Dispose();
         }
      }
   }
}