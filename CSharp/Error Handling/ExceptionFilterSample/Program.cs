/*
 * Exception filters
 */

using static System.Console;

namespace ExceptionFilterSample
{
   public static class Program
   {
      public static void Main()
      {
         try
         {
            ThrowWithErrorCode(405);
         }
         catch (CustomException customEx) when (customEx.ErrorCode == 405)
         {
            WriteLine($"Exception caught with filter {customEx.Message} and {customEx.ErrorCode}");
         }
         catch (CustomException customEx)
         {
            WriteLine($"Exception caught {customEx.Message} and {customEx.ErrorCode}");
         }
      }

      private static void ThrowWithErrorCode(int code)
      {
         throw new CustomException("Error In Foo") {ErrorCode = code};
      }
   }
}