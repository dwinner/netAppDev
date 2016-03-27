/*
 * Информация об исключении
 */
using System;
using System.Collections;

namespace HowToCSharp.Ch04.PrintExceptionInfo
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            DivideByZero();
         }
         catch (DivideByZeroException ex)
         {
            Console.WriteLine("ToString(): {0}", ex);
            Console.WriteLine("Message: {0}", ex.Message);
            Console.WriteLine("Source: {0}", ex.Source);
            Console.WriteLine("HelpLink: {0}", ex.HelpLink);
            Console.WriteLine("TargetSite: {0}", ex.TargetSite);
            Console.WriteLine("Inner Exception: {0}", ex.InnerException);
            Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
            Console.WriteLine("Data:");
            foreach (DictionaryEntry entry in ex.Data)
            {
               Console.WriteLine("\t{0}: {1}", entry.Key, entry.Value);
            }
         }
         Console.ReadKey();
      }

      private static void DivideByZero()
      {
         int divisor = 0;
         Console.WriteLine("{0}", 13 / divisor);
      }
   }
}
