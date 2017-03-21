using System;
using System.Runtime.Serialization;

namespace AppDevUnited.SelfTester.Model
{
   [Serializable]
   public class FailDirPatternComponentsException : Exception
   {
      public FailDirPatternComponentsException()
      {
      }

      public FailDirPatternComponentsException(string message)
         : base(message)
      {
      }

      public FailDirPatternComponentsException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected FailDirPatternComponentsException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }

   [Serializable]
   public class InvalidSettingsException : Exception
   {
      public InvalidSettingsException()
      {
      }

      public InvalidSettingsException(string message)
         : base(message)
      {
      }

      public InvalidSettingsException(string message, Exception inner)
         : base(message, inner)
      {
      }

      protected InvalidSettingsException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
      }
   }
}