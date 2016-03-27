using System;
using System.Runtime.Serialization;

namespace CustomConfigSectionSample
{
   /// <summary>
   ///    Исключение, связанное с ошибкой в файле конфигурации для социальных сетей
   /// </summary>
   [Serializable]
   public class SocialConfigurationException : Exception
   {
      public SocialConfigurationException()
      {
      }

      public SocialConfigurationException(string message)
         : base(message)
      {
      }

      public SocialConfigurationException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected SocialConfigurationException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}