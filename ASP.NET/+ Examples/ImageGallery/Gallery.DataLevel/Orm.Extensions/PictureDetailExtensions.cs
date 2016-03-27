using System;
using JpegMetadata.Library;

namespace Gallery.DataLevel.Orm.Extensions
{
   /// <summary>
   /// Вспомогательный класс для обработки дополнительной информации об изображении
   /// </summary>
   public static class PictureDetailExtensions
   {
      /// <summary>
      /// Копирование дополнительной информации из метаданных
      /// </summary>
      /// <param name="pictureDetail">Дополнительная информация</param>
      /// <param name="jpegMetadata">Метаданные</param>
      public static void CopyMasterData(this PictureDetail pictureDetail, JpegMetadataInfo jpegMetadata)
      {
         pictureDetail.CameraManufacturer = jpegMetadata.CameraManufacturer;
         pictureDetail.CameraModel = jpegMetadata.CameraModel;
         pictureDetail.CompressedBitsPerPixel = jpegMetadata.CompressedBitsPerPixel;
         pictureDetail.ExposureTime = jpegMetadata.ExposureTime;
         pictureDetail.FNumber = jpegMetadata.FNumber;
         pictureDetail.FocalLength = jpegMetadata.FocalLength;
         pictureDetail.ShootingDate = ParseShootingDate(jpegMetadata.ShootingDate);
         pictureDetail.ShutterSpeedValue = jpegMetadata.ShutterSpeedValue;
      }

      /// <summary>
      /// Установка дополнительной информации в значения по-умолчанию
      /// </summary>
      /// <param name="pictureDetail">Дополнительная информация</param>
      public static void SetDefaultMasterData(this PictureDetail pictureDetail)
      {
         pictureDetail.CameraManufacturer = null;
         pictureDetail.CameraModel = null;
         pictureDetail.CompressedBitsPerPixel = null;
         pictureDetail.ExposureTime = null;
         pictureDetail.FNumber = null;
         pictureDetail.FocalLength = null;
         pictureDetail.ShootingDate = null;
         pictureDetail.ShutterSpeedValue = null;
      }

      /// <summary>
      /// Разбор даты съемки из строки метаданных
      /// </summary>
      /// <param name="shootingDate">Дата съемки</param>
      /// <returns>Дата съемки</returns>
      private static DateTime? ParseShootingDate(string shootingDate)
      {
         if (string.IsNullOrWhiteSpace(shootingDate))
         {
            return null;
         }

         string[] dateTimeComponents = shootingDate.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
         if (dateTimeComponents.Length != 2)
         {
            return null;
         }

         string[] dateComponents = dateTimeComponents[0].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
         string[] timeComponents = dateTimeComponents[1].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
         if (dateComponents.Length != 3 || timeComponents.Length != 3)
         {
            return null;
         }

         try
         {
            int year = int.Parse(dateComponents[0]);
            int month = int.Parse(dateComponents[1]);
            int day = int.Parse(dateComponents[2]);
            int hours = int.Parse(timeComponents[0]);
            int minutes = int.Parse(timeComponents[1]);
            int seconds = int.Parse(timeComponents[2]);

            return new DateTime(year, month, day, hours, minutes, seconds);
         }
         catch (Exception)
         {
            return null;
         }
      }
   }
}