using System;
using System.Runtime.Serialization;

namespace DynConfReading
{
   [Serializable]
   public class InvalidSettingsException : Exception
   {
      public InvalidSettingsException()
      {
      }

      public InvalidSettingsException(string message) : base(message)
      {
      }

      public InvalidSettingsException(string message, Exception inner) : base(message, inner)
      {
      }

      protected InvalidSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
   }
}