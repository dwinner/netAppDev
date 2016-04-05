// Прозрачный контроль над ошибками

using System;

namespace HandlingExceptions
{
   internal static class Program
   {
      private static void Main()
      {
         RaiseError();
      }

      [ExceptionDialog(Caption = "Oops", Message = "Oops message")]
      private static void RaiseError()
      {
         throw new Exception("Ooops");
      }
   }
}