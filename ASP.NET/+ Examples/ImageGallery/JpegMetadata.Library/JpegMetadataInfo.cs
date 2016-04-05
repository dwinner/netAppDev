using System;
using System.Collections.Generic;

namespace JpegMetadata.Library
{
   /// <summary>
   /// Структура для некоторых метаданных изображения
   /// </summary>
   public struct JpegMetadataInfo : IEquatable<JpegMetadataInfo>
   {
      /// <summary>
      /// Имя файла
      /// </summary>
      public string FileName { get; set; }

      /// <summary>
      /// Дата съемки
      /// </summary>
      public string ShootingDate { get; set; }

      /// <summary>
      /// Камера, изготовитель
      /// </summary>
      public string CameraManufacturer { get; set; }

      /// <summary>
      /// Модель фотоаппарата
      /// </summary>
      public string CameraModel { get; set; }

      /// <summary>
      /// Фокусное расстояние в мм.
      /// </summary>
      public int? FocalLength { get; set; }

      /// <summary>
      /// Скорость выдержки, сек.
      /// </summary>
      public float? ExposureTime { get; set; }

      /// <summary>
      /// Значение выдержки
      /// </summary>
      public float? ShutterSpeedValue { get; set; }

      /// <summary>
      /// Диафрагма f/FNumber
      /// </summary>
      public int? FNumber { get; set; }

      /// <summary>
      /// Коэффициент сжатия (bit/pixel)
      /// </summary>
      public string CompressedBitsPerPixel { get; set; }

      /// <summary>
      /// Размеры изображения [ширина, высота]
      /// </summary>
      public Tuple<int, int> Dimensions { get; set; }

      public override string ToString()
      {
         return
            string.Format(
               "FileName: {0}, ShootingDate: {1}, CameraManufacturer: {2}, CameraModel: {3}, FocalLength: {4}, ExposureTime: {5}, ShutterSpeedValue: {6}, FNumber: {7}, CompressedBitsPerPixel: {8}, Dimensions: {9}",
               FileName, ShootingDate, CameraManufacturer, CameraModel, FocalLength, ExposureTime, ShutterSpeedValue,
               FNumber, CompressedBitsPerPixel, Dimensions);
      }

      public bool Equals(JpegMetadataInfo other)
      {
         return string.Equals(FileName, other.FileName) && string.Equals(ShootingDate, other.ShootingDate) &&
                string.Equals(CameraManufacturer, other.CameraManufacturer) && FocalLength == other.FocalLength &&
                string.Equals(CameraModel, other.CameraModel) && ExposureTime.Equals(other.ExposureTime) &&
                ShutterSpeedValue.Equals(other.ShutterSpeedValue) && FNumber == other.FNumber &&
                string.Equals(CompressedBitsPerPixel, other.CompressedBitsPerPixel) &&
                Equals(Dimensions, other.Dimensions);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         return obj is JpegMetadataInfo && Equals((JpegMetadataInfo)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = (FileName != null ? FileName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (ShootingDate != null ? ShootingDate.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (CameraManufacturer != null ? CameraManufacturer.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ FocalLength.GetHashCode();
            hashCode = (hashCode * 397) ^ (CameraModel != null ? CameraModel.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ ExposureTime.GetHashCode();
            hashCode = (hashCode * 397) ^ ShutterSpeedValue.GetHashCode();
            hashCode = (hashCode * 397) ^ FNumber.GetHashCode();
            hashCode = (hashCode * 397) ^ (CompressedBitsPerPixel != null ? CompressedBitsPerPixel.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Dimensions != null ? Dimensions.GetHashCode() : 0);
            return hashCode;
         }
      }

      public static bool operator ==(JpegMetadataInfo left, JpegMetadataInfo right)
      {
         return left.Equals(right);
      }

      public static bool operator !=(JpegMetadataInfo left, JpegMetadataInfo right)
      {
         return !left.Equals(right);
      }

      private sealed class JpegMetadataEqualityComparer : IEqualityComparer<JpegMetadataInfo>
      {
         public bool Equals(JpegMetadataInfo x, JpegMetadataInfo y)
         {
            return x.Equals(y);            
         }

         public int GetHashCode(JpegMetadataInfo obj)
         {
            return GetHashCode();            
         }
      }

      private static readonly IEqualityComparer<JpegMetadataInfo> JpegMetadataInfoComparerInstance = new JpegMetadataEqualityComparer();

      public static IEqualityComparer<JpegMetadataInfo> JpegMetadataInfoComparer
      {
         get { return JpegMetadataInfoComparerInstance; }
      }
   }
}