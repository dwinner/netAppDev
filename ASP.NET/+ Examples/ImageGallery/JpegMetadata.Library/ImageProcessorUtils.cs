using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace JpegMetadata.Library
{
   /// <summary>
   /// Вспомогательный класс для обработки изображений
   /// </summary>
   public static class ImageProcessorUtils
   {
      private const int DefaultMemoryStreamCapacity = 0x2dc6c0;
      private const long DefaultCompressionQuality = 55L;

      /// <summary>
      /// Сжимает изображение в байтовый массив
      /// </summary>
      /// <param name="fileName">Имя файла с изображением</param>
      /// <param name="squareFitWidth">Ширина вписывающего квадрата</param>
      /// <param name="interpolationMode">Алгоритм масштабирования</param>
      /// <param name="smoothingMode">Режим сглаживания</param>
      /// <param name="compositingQuality">Уровень качества сжатия</param>
      /// <returns>Массив байтов сжатого изображения</returns>
      /// <exception cref="FailCompressException">Если не удалось сжать изображение</exception>
      public static byte[] ResizePicture(string fileName, int squareFitWidth,
         InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic,
         SmoothingMode smoothingMode = SmoothingMode.HighSpeed,
         CompositingQuality compositingQuality = CompositingQuality.Default)
      {
         try
         {
            using (var memoryStream = new MemoryStream())
            {
               using (var sourceBitmap = new Bitmap(fileName))
               {
                  return GetConversionBytes(squareFitWidth, interpolationMode, smoothingMode, compositingQuality,
                     sourceBitmap, memoryStream);
               }
            }
         }
         catch (Exception ex)
         {
            throw new FailCompressException("Fail compression", ex);
         }
      }

      /// <summary>
      /// Изменяет размер исходного изображения
      /// </summary>
      /// <param name="imageStream">Входной поток данных изображения</param>
      /// <param name="squareFitWidth">Ширина вписывающего квадрата</param>
      /// <param name="interpolationMode">Алгоритм масштабирования</param>
      /// <param name="smoothingMode">Режим сглаживания</param>
      /// <param name="compositingQuality">Уровень качества сжатия</param>
      /// <returns>Массив байтов нового изображения</returns>
      /// <exception cref="FailCompressException">Если не удалось сжать изображение</exception>
      public static byte[] ResizePicture(Stream imageStream, int squareFitWidth,
         InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic,
         SmoothingMode smoothingMode = SmoothingMode.HighSpeed,
         CompositingQuality compositingQuality = CompositingQuality.Default)
      {
         try
         {
            using (var memoryStream = new MemoryStream(DefaultMemoryStreamCapacity))
            {
               using (var sourceBitmap = new Bitmap(imageStream))
               {
                  return GetConversionBytes(squareFitWidth, interpolationMode, smoothingMode, compositingQuality,
                     sourceBitmap, memoryStream);
               }
            }
         }
         catch (Exception ex)
         {
            throw new FailCompressException("Fail compression", ex);
         }
      }

      /// <summary>
      /// Пропорциональный пересчет размеров изображения в зависимости от вписывающего квадрата
      /// </summary>
      /// <param name="oldDimensions">Исходные размеры</param>
      /// <param name="squareFitWidth">Ширина вписывающего квадрата</param>
      /// <returns>Пересчитанные размеры</returns>
      public static Tuple<int, int> ResampleDimensions(Tuple<int, int> oldDimensions, int squareFitWidth)
      {
         int oldWidth = oldDimensions.Item1;
         int oldHeight = oldDimensions.Item2;
         float ratio = (float)oldWidth / oldHeight;
         int newWidth, newHeight;
         if (ratio < 1)
         {
            newWidth = (int)Math.Ceiling(squareFitWidth * ratio);
            newHeight = squareFitWidth;
         }
         else
         {
            newHeight = (int)Math.Ceiling(squareFitWidth / ratio);
            newWidth = squareFitWidth;
         }

         return Tuple.Create(newWidth, newHeight);         
      }

      /// <summary>
      /// Получение размеров изображения
      /// </summary>
      /// <param name="fileName">Имя файла изображения</param>
      /// <param name="width">Ширина</param>
      /// <param name="height">Высота</param>
      /// <exception cref="ArgumentException">Если не удалось прочитать пиксели из файла</exception>
      /// <exception cref="FileNotFoundException">Если файл не найден</exception>
      public static void GetImageDimensions(string fileName, out int width, out int height)
      {
         using (var bitmap = new Bitmap(fileName))
         {
            width = bitmap.Width;
            height = bitmap.Height;
         }
      }

      /// <summary>
      /// Получение размеров изображения
      /// </summary>
      /// <param name="stream">Поток данных</param>
      /// <param name="width">Ширина</param>
      /// <param name="height">Высота</param>
      /// <exception cref="ArgumentException">Если не удалось прочитать пиксели из файла</exception>
      public static void GetImageDimensions(Stream stream, out int width, out int height)
      {
         using (var bitmap = new Bitmap(stream))
         {
            width = bitmap.Width;
            height = bitmap.Height;
         }
      }

      /// <summary>
      /// Сжимает изображение в соответствии с указанным качеством
      /// </summary>
      /// <param name="imageStream">Входной поток изображения</param>
      /// <param name="newCompressionQuality">
      ///   Качество сжатия (от 0 до 100)
      ///   <remarks>0 - максимальное сжатие, 100 - минимальное сжатие</remarks>
      /// </param>
      /// <returns>Массив байтов сжатого изображения</returns>
      /// <exception cref="ArgumentOutOfRangeException">Если качество сжатие вышло за пределы допустимых</exception>
      public static byte[] ChangeCompressionQuality(Stream imageStream, long newCompressionQuality = DefaultCompressionQuality)
      {
         //Contract.Requires(newCompressionQuality >= 0 && newCompressionQuality <= 100);
         if (newCompressionQuality < 0 || newCompressionQuality > 100)
         {
            throw new ArgumentOutOfRangeException("newCompressionQuality");
         }

         using (var bitmap = new Bitmap(imageStream))
         {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, newCompressionQuality);
            using (var memoryStream = new MemoryStream(DefaultMemoryStreamCapacity))
            {
               ImageFormat imageFormat = ImageFormat.Jpeg;
               var imageCodecInfo =
                  ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == imageFormat.Guid);
               if (imageCodecInfo == null) return new byte[0];               
               bitmap.Save(memoryStream, imageCodecInfo, encoderParameters);
               return memoryStream.ToArray();
            }
         }
      }

      /// <summary>
      /// Сжимает изображение в соответствии с указанным качеством
      /// </summary>
      /// <param name="fileName">Имя файла изображения</param>
      /// <param name="newCompressionQuality">Качество сжатия (от 0 до 100)</param>
      /// <returns>Массив байтов сжатого изображения</returns>
      public static byte[] ChangeCompressionQuality(string fileName, long newCompressionQuality = DefaultCompressionQuality)
      {
         using (var fileStream = new FileStream(fileName, FileMode.Open))
         {
            return ChangeCompressionQuality(fileStream, newCompressionQuality);
         }
      }

      /// <summary>
      /// Получение нового массива байтов изображения 
      /// </summary>
      /// <param name="squareFitWidth">Ширина вписывающего квадрата</param>
      /// <param name="interpolationMode">Алгоритм масштабирования</param>
      /// <param name="smoothingMode">Режим сглаживания</param>
      /// <param name="compositingQuality">Уровень качества сжатия</param>
      /// <param name="sourceBitmap">Исходные пиксели</param>
      /// <param name="memoryStream">Поток данных в памяти</param>
      /// <returns>Нового массив байтов изображения </returns>
      private static byte[] GetConversionBytes(int squareFitWidth, InterpolationMode interpolationMode,
         SmoothingMode smoothingMode, CompositingQuality compositingQuality, Bitmap sourceBitmap, MemoryStream memoryStream)
      {
         Tuple<int, int> newDimensions = ResampleDimensions(Tuple.Create(sourceBitmap.Width, sourceBitmap.Height), squareFitWidth);
         using (var destinationBitmap = new Bitmap(newDimensions.Item1, newDimensions.Item2))
         {
            using (var graphicsContext = Graphics.FromImage(destinationBitmap))
            {
               graphicsContext.CompositingQuality = compositingQuality;
               graphicsContext.SmoothingMode = smoothingMode;
               graphicsContext.InterpolationMode = interpolationMode;
               graphicsContext.DrawImage(sourceBitmap, new Rectangle(0, 0, newDimensions.Item1, newDimensions.Item2));
               destinationBitmap.Save(memoryStream, ImageFormat.Jpeg);

               return memoryStream.ToArray();
            }
         }
      }
   }
}