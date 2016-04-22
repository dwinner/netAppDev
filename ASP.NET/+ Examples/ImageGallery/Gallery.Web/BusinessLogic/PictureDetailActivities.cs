using System.IO;
using System.Web;
using Gallery.DataLevel.Orm;
using Gallery.DataLevel.Orm.Extensions;
using JpegMetadata.Library;

namespace Gallery.Web.BusinessLogic
{
   /// <summary>
   /// Вспомогательный класс для логики работы с метаинформацией об изображении
   /// </summary>
   public static class PictureDetailActivities
   {
      /// <summary>
      /// Инициализация метаданных изображения
      /// </summary>
      /// <param name="picture">Изображение</param>
      /// <param name="imageFile">Загруженный файл изображения</param>
      /// <returns>Объект с метаданными изображения</returns>
      public static PictureDetail InitializePictureDetail(PictureGallery picture, HttpPostedFileBase imageFile)
      {
         var pictureDetail = new PictureDetail();
         Stream imageMemoryStream = null;
         try
         {
            imageMemoryStream = new MemoryStream(picture.PictureData);
            using (var pictureMetadata = new PictureMetadata(imageMemoryStream, imageFile.FileName))
            {
               var jpegMetadata = pictureMetadata.ReadJpegMetadata();
               pictureDetail.CopyMasterData(jpegMetadata);
            }
         }
         catch (JpegMetadataException)
         {
            pictureDetail.SetDefaultMasterData();
         }
         finally
         {
            if (imageMemoryStream != null)
            {
               imageMemoryStream.Close();
            }
         }

         return pictureDetail;
      }
   }
}