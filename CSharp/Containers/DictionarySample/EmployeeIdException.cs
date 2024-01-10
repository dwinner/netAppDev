using System;
using System.Runtime.Serialization;

namespace DictionarySample
{
   [Serializable]
   public class EmployeeIdException : Exception
   {
      public EmployeeIdException()
      {
      }

      public EmployeeIdException(string message) : base(message)
      {
      }

      public EmployeeIdException(string message, Exception inner) : base(message, inner)
      {
      }

      protected EmployeeIdException(
         SerializationInfo info,
         StreamingContext context) : base(info, context)
      {
      }
   }
}