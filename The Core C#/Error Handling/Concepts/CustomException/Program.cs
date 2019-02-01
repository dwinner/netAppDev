/**
 * Создание собственных классов исключений
 */

using System;
using System.Runtime.Serialization;

namespace _10_CustomException
{
   class Program
   {
      static void Main(string[] args)
      {
      }
   }

   [Serializable]
   public class EmployeeVerificationException : Exception
   {
      public enum Cause
      {
         InvalidSsn,
         InvalidBirthDate
      }

      public Cause Reason { get; private set; }

      public EmployeeVerificationException(Cause reason)
         : base()
      {
         Reason = reason;
      }

      public EmployeeVerificationException(Cause reason, string message)
         : base(message)
      {
         Reason = reason;
      }

      public EmployeeVerificationException(Cause reason, string message, Exception inner)
         : base(message, inner)
      {
         Reason = reason;
      }

      protected EmployeeVerificationException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }
   }
}
