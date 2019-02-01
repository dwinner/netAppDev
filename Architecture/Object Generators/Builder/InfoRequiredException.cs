using System;
using System.Runtime.Serialization;

namespace Builder
{
   /// <summary>
   ///    Исключение приложения о необходимости ввода информации.
   /// </summary>
   [Serializable]
   public class InfoRequiredException : ApplicationException
   {
      /// <summary>
      ///    Флаги ошибок.
      /// </summary>
      [Flags]
      public enum InfoErrors
      {
         None = 0x0,
         StartDateRequired = 0x1,
         EndDateRequired = 0x2,
         DescriptionRequired = 0x4,
         AttendeeRequired = 0x8,
         LocationRequired = 0x10
      }


      public InfoRequiredException()
      {
      }

      public InfoRequiredException(string message)
         : base(message)
      {
      }

      public InfoRequiredException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      protected InfoRequiredException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }

      public InfoRequiredException(string message, InfoErrors infoRequired)
         : base(message)
      {
         InfoRequired = infoRequired;
      }

      public InfoErrors InfoRequired { get; }
   }
}