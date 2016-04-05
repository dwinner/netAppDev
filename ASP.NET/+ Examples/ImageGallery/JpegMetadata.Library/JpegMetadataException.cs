using System;
using System.Runtime.Serialization;

namespace JpegMetadata.Library
{
   /// <summary>
   /// Исключение, возникающее при неудачной попытке чтения метаданных изображения в Jpeg формате
   /// </summary>
   [Serializable]
   public class JpegMetadataException : Exception
   {
      public JpegMetadataException()
      {
      }

      public JpegMetadataException(string message)
         : base(message)
      {
      }

      public JpegMetadataException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected JpegMetadataException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}