/**
 * Перехват необработанных исключений в текущем домене приложения
 */
using System;

namespace HowToCSharp.Ch04.UnhandledException
{
   class Program
   {
      static void Main()
      {
         AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
         throw new InvalidOperationException("Oops");
      }

      static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
      {
         Console.WriteLine("Caught unhandled exception: {0}",
            (e.IsTerminating ? "which will terminate the programm" : string.Empty));
         Console.WriteLine(e.ExceptionObject.ToString());
      }
   }
}
