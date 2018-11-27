using System.IO;
using Android.Graphics;
using Android.OS;
using Java.Text;
using Java.Util;
using JFile = Java.IO.File;

namespace PointOfViewApp.Poco
{
   public static class PointOfInterestExtensions
   {
      internal const string Authorities = "AppDevUnited.PoiApp.FileProvider";

      public static Bitmap GetImage(this PointOfInterest interest) => interest.Id.GetImage();

      private static Bitmap GetImage(this int poiId)
      {
         var fileName = poiId.GetPictureFile().AbsolutePath;
         if (File.Exists(fileName))
         {
            var imageFile = new JFile(fileName);
            return BitmapFactory.DecodeFile(imageFile.Path);
         }

         return null;
      }

      public static void DeleteImage(this int poiId)
      {
         var filePath = poiId.GetPictureFile().AbsolutePath;
         if (File.Exists(filePath))
         {
            File.Delete(filePath);
         }
      }

      public static JFile GetPictureFile(this int poiId)
      {
         var timeStamp = new SimpleDateFormat("yyyyMMddHHmmss").Format(new Date());
         var pictureFile = $"ZOFTINO_{timeStamp}{poiId}";
         var storageDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
         var image = JFile.CreateTempFile(pictureFile, ".jpg", storageDir);

         return image;
      }
   }
}