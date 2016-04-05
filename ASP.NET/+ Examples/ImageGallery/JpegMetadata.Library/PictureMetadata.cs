using System;
using System.Collections.Generic;
using System.IO;
using ExifLib;

namespace JpegMetadata.Library
{
   /// <summary>
   /// Класс для хранения метаданных об изображении
   /// </summary>
   public sealed class PictureMetadata : IDisposable
   {
      private readonly string _fileName;

      private readonly ExifReader _exifReader;
      private bool _isDisposed;
      private const string FailReadingExif = "Fail reading EXIF Metadata";

      /// <summary>
      /// Имя файла изображения
      /// </summary>
      public string FileName
      {
         get { return _fileName; }
      }

      /// <summary>
      /// Конструктор класса для хранения метаданных об изображении
      /// </summary>
      /// <param name="fileName">Имя файла изображения</param>
      /// <exception cref="JpegMetadataException">Если не найден файл или он не формата JPEG</exception>
      public PictureMetadata(string fileName)
      {
         if (!File.Exists(fileName))
         {
            throw new JpegMetadataException(FailReadingExif,
               new FileNotFoundException(string.Format("File {0} is not found", fileName), fileName));
         }

         try
         {
            _exifReader = new ExifReader(fileName);
         }
         catch (ExifLibException exifLibEx)
         {
            throw new JpegMetadataException(FailReadingExif, exifLibEx);
         }

         _fileName = fileName;
      }

      /// <summary>
      /// Конструктор класса для хранения метаданных об изображении
      /// </summary>
      /// <param name="imageStream">Входной поток данных изображения</param>
      /// <param name="fileName">Имя файла изображения</param>
      public PictureMetadata(Stream imageStream, string fileName)
      {
         _fileName = fileName;
         try
         {
            _exifReader = new ExifReader(imageStream);
         }
         catch (ExifLibException exifLibEx)
         {
            throw new JpegMetadataException(FailReadingExif, exifLibEx);
         }
      }

      /// <summary>
      /// Чтение метаданных из изображения JPEG      
      /// </summary>
      /// <returns>Метаданные изображения</returns>
      public JpegMetadataInfo ReadJpegMetadata()
      {
         if (_isDisposed)
         {
            throw new ObjectDisposedException(ToString());
         }

         object shotingDate,
            cameraManufacturer,
            cameraModel,
            focalLength,
            exposureTimeObj,
            fnumberObj,
            bitsPerPixel,
            shutterSpeedValueObj,
            imgWidthObj,
            imgHeightObj;
         int? intFocalLength;
         double? exposureTime;
         double? shutterSpeedValue;
         int? fnumber;
         int imageWidth, imageHeight;

         _exifReader.GetTagValue(ExifTags.DateTimeOriginal, out shotingDate);
         _exifReader.GetTagValue(ExifTags.Make, out cameraManufacturer);
         _exifReader.GetTagValue(ExifTags.Model, out cameraModel);
         _exifReader.GetTagValue(ExifTags.FocalLength, out focalLength);
         _exifReader.GetTagValue(ExifTags.ExposureTime, out exposureTimeObj);
         _exifReader.GetTagValue(ExifTags.ShutterSpeedValue, out shutterSpeedValueObj);
         _exifReader.GetTagValue(ExifTags.FNumber, out fnumberObj);
         _exifReader.GetTagValue(ExifTags.CompressedBitsPerPixel, out bitsPerPixel);
         _exifReader.GetTagValue(ExifTags.ImageWidth, out imgWidthObj);
         _exifReader.GetTagValue(ExifTags.ImageLength, out imgHeightObj);

         if (focalLength == null)
         {
            intFocalLength = null;
         }
         else
         {
            int tempFocalLen;
            intFocalLength = int.TryParse(focalLength.ToString(), out tempFocalLen) ? (int?)tempFocalLen : null;
         }

         if (exposureTimeObj == null)
         {
            exposureTime = null;
         }
         else
         {
            double tempExposure;
            exposureTime = double.TryParse(exposureTimeObj.ToString(), out tempExposure) ? (double?)tempExposure : null;
         }

         if (fnumberObj == null)
         {
            fnumber = null;
         }
         else
         {
            int fnum;
            fnumber = int.TryParse(fnumberObj.ToString(), out fnum) ? (int?)fnum : null;
         }

         if (shutterSpeedValueObj == null)
         {
            shutterSpeedValue = null;
         }
         else
         {
            double tmpSpeedVal;
            shutterSpeedValue = double.TryParse(shutterSpeedValueObj.ToString(), out tmpSpeedVal)
               ? (double?)tmpSpeedVal
               : null;
         }

         if (imgWidthObj == null)
         {
            imageWidth = 0;
         }
         else
         {
            int lImgWidth;
            imageWidth = int.TryParse(imgWidthObj.ToString(), out lImgWidth) ? lImgWidth : 0;
         }

         if (imgHeightObj == null)
         {
            imageHeight = 0;
         }
         else
         {
            int lImgHeight;
            imageHeight = int.TryParse(imgHeightObj.ToString(), out lImgHeight) ? lImgHeight : 0;
         }

         return new JpegMetadataInfo
         {
            FileName = _fileName,
            ShootingDate = shotingDate == null ? string.Empty : shotingDate.ToString(),
            CameraManufacturer = cameraManufacturer == null ? string.Empty : cameraManufacturer.ToString(),
            CameraModel = cameraModel == null ? string.Empty : cameraModel.ToString(),
            FocalLength = intFocalLength,
            ExposureTime = (float?)exposureTime,
            ShutterSpeedValue = (float?)shutterSpeedValue,
            FNumber = fnumber,
            CompressedBitsPerPixel = bitsPerPixel == null ? string.Empty : bitsPerPixel.ToString(),
            Dimensions = Tuple.Create(imageWidth, imageHeight)
         };
      }

      /// <summary>
      /// Чтение всех метаданных из JPEG
      /// </summary>
      /// <returns>Словарь всех метаданных, которые удалось прочитать</returns>
      public IDictionary<string, object> ReadAllJpegMetadata()
      {
         if (_isDisposed)
         {
            throw new ObjectDisposedException(ToString());
         }

         Array exifTags = Enum.GetValues(typeof(ExifTags));
         var exifMetadata = new Dictionary<string, object>(exifTags.Length);
         foreach (ExifTags exifTag in exifTags)
         {
            object result;
            bool hasTagValue = _exifReader.GetTagValue(exifTag, out result);
            object procResult = hasTagValue ? result ?? string.Empty : string.Empty;
            exifMetadata.Add(exifTag.ToString(), procResult);
         }

         return exifMetadata;
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      ~PictureMetadata()
      {
         Dispose(false);
      }

      internal void Dispose(bool disposing)
      {
         if (!_isDisposed && disposing && _exifReader != null)
         {
            _exifReader.Dispose();
         }

         _isDisposed = true;
      }
   }
}