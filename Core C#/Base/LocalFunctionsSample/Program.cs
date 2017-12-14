/**
 * Local functions
 */

using static System.Console;

namespace LocalFunctionsSample
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         void SayHi()
         {
            WriteLine("Hi");
         }

         int ReturnInt() => 1;

         SayHi();
         WriteLine(ReturnInt());
      }
   }
}