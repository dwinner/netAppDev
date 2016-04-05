/**
 * Гарантия освобождения ресурсов после сбоев.
 */
using System;
using System.IO;

namespace HowToCSharp.Ch04.TryFinallyFile
{
   class Program
   {
      static void Main(string[] args)
      {
         StreamWriter stream = null;
         try
         {
            stream = File.CreateText("temp.txt");
            stream.Write(null, -1, 1);
         }
         catch (ArgumentNullException ex)
         {
            Console.WriteLine("In catch: ");
            Console.WriteLine(ex.Message);
         }
         finally
         {
            Console.WriteLine("In finally: Closing file");
            if (stream != null)
            {
               stream.Close();
            }
         }
         Console.ReadKey();
      }
   }
}
