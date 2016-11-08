using System;

namespace AcmeCarRental.AopVersion.Data
{
   public static class Exceptions
   {
      public static bool Handle(Exception ex)
      {
         // this code could check to see if it's a type
         // of exception that can be handled
         // or maybe it can notify the user
         // or system admins
         // etc

         return ex.GetType() != typeof (ArithmeticException) && ex.GetType() == typeof (TimeoutException);
      }
   }
}