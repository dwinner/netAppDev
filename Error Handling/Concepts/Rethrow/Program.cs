/**
 * Сохранение/потеря трассировки в стеке вызовов
 */
using System;

namespace HowToCSharp.Ch04.Rethrow
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            RethrowWithNoPreservation();
         }
         catch (ArgumentException ex)
         {
            Console.WriteLine("Stack trace from rethrow (no stack preservation):");
            Console.WriteLine(ex.StackTrace);
         }

         Console.WriteLine();

         try
         {
            RethrowWithPreservation();
         }
         catch (ArgumentException ex)
         {
            Console.WriteLine("Stack trace from rethrow (stack preservation):");
            Console.WriteLine(ex.StackTrace);
         }

         Console.ReadKey();
      }

      private static void DoSomething()
      {
         throw new ArgumentException("This function has no arguments!");
      }

      private static void RethrowWithNoPreservation()
      {
         try
         {
            DoSomething();
         }
         catch (ArgumentException ex)
         {
            Console.WriteLine(ex);
            throw ex;   // NOTE: Потеря следа в стеке вызовов
         }
      }

      private static void RethrowWithPreservation()
      {
         try
         {
            DoSomething();
         }
         catch (ArgumentException ex)
         {
            Console.WriteLine(ex);
            throw;   // NOTE: Сохранение следа в стеке вызовов
         }
      }
   }
}
