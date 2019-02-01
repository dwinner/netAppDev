using System;

namespace GenericPrimAndProperCarEvents
{
   public class CarEventArgs : EventArgs
   {
      public readonly string msg;
      public CarEventArgs(string message)
      {
         msg = message;
      }
   }

}
