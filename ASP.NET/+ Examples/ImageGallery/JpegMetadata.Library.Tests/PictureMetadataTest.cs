using System;
using NUnit.Framework;

namespace JpegMetadata.Library.Tests
{
   /// <summary>
   /// Тест на чтение метаданных изображения
   /// </summary>
   public class PictureMetadataTest
   {
      private const string PngFileName = "pictures/Zebrous.png";
      private const string JpegFileName = "pictures/Corp.jpg";

      /// <summary>
      /// Чтение метаданных из JPEG
      /// </summary>
      [Test]
      public void ReadJpegMetadataTest()
      {
         ReadMetadata(JpegFileName);
      }

      /// <summary>
      /// Чтение метаданных из PNG должно провалиться
      /// </summary>
      [Test]
      [ExpectedException(typeof(JpegMetadataException))]
      public void ReadPngMetadataTest()
      {
         ReadMetadata(PngFileName);
      }

      /// <summary>
      /// Чтение метаданных
      /// </summary>
      /// <param name="fileName">Имя файла</param>
      private static void ReadMetadata(string fileName)
      {
         using (var pictureMetadata = new PictureMetadata(fileName))
         {
            JpegMetadataInfo jpegMetadata = pictureMetadata.ReadJpegMetadata();
            Console.WriteLine("Изготовитель камеры: {0}", jpegMetadata.CameraManufacturer);
            Console.WriteLine("Модель фотоаппарата: {0}", jpegMetadata.CameraModel);
            Console.WriteLine("Коэффициент сжатия: {0}", jpegMetadata.CompressedBitsPerPixel);
            Console.WriteLine("Размеры изображения: {0}", jpegMetadata.Dimensions);
            Console.WriteLine("Скорость выдержки, сек: {0}", jpegMetadata.ExposureTime);
            Console.WriteLine("Диафрагма: {0}", jpegMetadata.FNumber);
            Console.WriteLine("Имя файла: {0}", jpegMetadata.FileName);
            Console.WriteLine("Фокусное расстояние в мм: {0}", jpegMetadata.FocalLength);
            Console.WriteLine("Дата съемки: {0}", jpegMetadata.ShootingDate);
            Console.WriteLine("Значение выдержки: {0}", jpegMetadata.ShutterSpeedValue);
         }
      }
   }
}