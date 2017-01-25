/**
 * Local functions
 */

using static System.Console;

namespace LocalFunctionsSample
{
   class Program
   {
      static void Main(string[] args)
      {
         void SayHi()
         {
            WriteLine("Hi");
         }

         int ReturnInt()
         {
            return 1;
         }                 

         SayHi();
         WriteLine(ReturnInt());
      }
   }
}
