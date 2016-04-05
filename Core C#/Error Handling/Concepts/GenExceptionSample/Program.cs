/**
 * Обобщенный класс исключений
 */

using System;

namespace GenExceptionSample
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            throw new GenException<DiskFullExceptionArgs>(
               new DiskFullExceptionArgs(@"C:\"), "The disk is full");
         }
         catch (GenException<DiskFullExceptionArgs> genEx)
         {
            Console.WriteLine(genEx.Message);
            Console.WriteLine(genEx.ExceptionArgs.DiskPath);
         }
      }
   }
}