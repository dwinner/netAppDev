/**
 * Демонстрация обработки исключений
 */

using System;
using System.IO;

namespace SolicitColdCall
{
   internal static class Program
   {
      private static void Main()
      {
         AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

         Console.Write("Please type in the name of the file containing the names of the people to be cold called > ");
         string fileName = Console.ReadLine();
         var peopleToRing = new ColdCallFileReader();

         try
         {
            peopleToRing.Open(fileName);
            for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
            {
               peopleToRing.ProcessNextPerson();
            }
            Console.WriteLine("All callers processed correctly");
         }
         catch (FileNotFoundException)
         {
            Console.WriteLine("The file {0} does not exist", fileName);
         }
         catch (ColdCallFileFormatException ex)
         {
            Console.WriteLine("The file {0} appears to have been corrupted", fileName);
            Console.WriteLine("Details of problem are: {0}", ex.Message);
            if (ex.InnerException != null)
            {
               Console.WriteLine("Inner exception was: {0}", ex.InnerException.Message);
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Exception occurred:\n{0}", ex.Message);
         }
         finally
         {
            peopleToRing.Dispose();
         }

         Console.ReadLine();
      }

      static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
      {
         var unhandledEx = e.ExceptionObject as Exception;
         if (unhandledEx != null)
         {
            Console.WriteLine(unhandledEx.Message);
         }
      }
   }
}