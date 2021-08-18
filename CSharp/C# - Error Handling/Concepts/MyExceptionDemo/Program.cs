using System;

namespace HowToCSharp.Ch04.MyExceptionDemo
{
   class Program
   {
      static void Main(string[] args)
      {
         CustomException customEx = new CustomException(13.0, "My very own exception!");
         Console.WriteLine(customEx.ToString());
         Console.WriteLine("Value: " + customEx.ExceptionData);
         Console.ReadKey();
      }
   }
}
