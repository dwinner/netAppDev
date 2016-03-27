using System;
using System.Runtime.Serialization;

namespace JpegMetadata.Library
{
   /// <summary>
   /// Исключение, возникающее при ошибках сжатия изображений
   /// </summary>
   [Serializable]
   public class FailCompressException : Exception
   {
      public FailCompressException()
      {
      }

      public FailCompressException(string message)
         : base(message)
      {
      }

      public FailCompressException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected FailCompressException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}