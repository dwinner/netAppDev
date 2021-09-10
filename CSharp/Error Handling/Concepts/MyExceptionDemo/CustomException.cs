/*
 * Создание собственных исключений
 */
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace HowToCSharp.Ch04.MyExceptionDemo
{
   [Serializable]
   public class CustomException : Exception
   {
      public double ExceptionData { get; private set; }

      public CustomException()
      {
         ExceptionData = 0.0;
      }

      public CustomException(string message)
         : base(message)
      {
         ExceptionData = 0.0;
      }

      public CustomException(string message, Exception innerException)
         : base(message, innerException)
      {
         ExceptionData = 0.0;
      }

      public CustomException(double exceptionData, string message)
         : base(message)
      {
         ExceptionData = exceptionData;
      }

      public CustomException(double exceptionData, string message, Exception innerException)
         : base(message, innerException)
      {
         ExceptionData = exceptionData;
      }

      // Методы сериализацтт
      protected CustomException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      {
         // Десериализация
         ExceptionData = info.GetDouble("MyExceptionData");
      }

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
         // Сериализация
         base.GetObjectData(info, context);
         info.AddValue("MyExceptionData", ExceptionData);
      }
   }
}
