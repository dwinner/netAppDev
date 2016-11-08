// Контракты кода

using System;

namespace PsCodeContracts
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            var reverser = new NameReverser();
            var returnValue = reverser.For("Sam");
            Console.WriteLine("Return: {0}", returnValue);
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.ToString());
         }
      }
   }
}