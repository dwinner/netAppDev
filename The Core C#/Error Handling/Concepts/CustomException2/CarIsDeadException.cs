using System;
using System.Runtime.Serialization;

namespace CustomException
{
   #region Первый способ!
   /*This custom exception describes the details of the car-is-dead condition.
    public class CarIsDeadException : ApplicationException
    {
       private string messageDetails = String.Empty;
       public DateTime ErrorTimeStamp { get; set; }
       public string CauseOfError { get; set; }
       public CarIsDeadException() { }
       public CarIsDeadException(string message, string cause, DateTime time)
       {
          messageDetails = message;
          CauseOfError = cause;
          ErrorTimeStamp = time;
       }

       // Override the Exception.Message property.
       public override string Message
       {
          get
          {
             return string.Format("Car Error Message: {0}", messageDetails);
          }
       }
    }*/
   #endregion

   #region Способ второй!
   /*public class CarIsDeadException : ApplicationException
   {
      public DateTime ErrorTimeStamp { get; set; }
      public string CauseOfError { get; set; }

      public CarIsDeadException() { }

      // Feed message to parent constructor.
      public CarIsDeadException(string message, string cause, DateTime time)
         : base(message)
      {
         CauseOfError = cause;
         ErrorTimeStamp = time;
      }
   }*/
   #endregion

   #region Способ третий!
   [global::System.Serializable]
   public class CarIsDeadException : ApplicationException
   {
      public CarIsDeadException() { }

      public CarIsDeadException(string message)
         : base(message) { }

      public CarIsDeadException(string message, Exception inner)
         : base(message, inner) { }

      protected CarIsDeadException(SerializationInfo info, StreamingContext context)
         : base(info, context) { }

      // Свои свойства для исключения
      public DateTime ErrorTimeStamp { get; set; }

      public string CauseOfError { get; set; }

      public CarIsDeadException(string message, string cause, DateTime time)
         : base(message)
      {
         CauseOfError = cause;
         ErrorTimeStamp = time;
      }
   }
   #endregion
}
