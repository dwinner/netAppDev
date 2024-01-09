/*
 * Проверка кол-ва свободного места в стеке
 */

using System;
using System.Runtime.CompilerServices;

namespace EnsureStackCapacity
{
   internal static class Program
   {
      private static void Main()
      {
         try
         {
            ForseOverflow(true);
         }
         catch (InsufficientExecutionStackException insufficientExecutionStackEx)
         {
            // NOTE: Это исключение поможет понять, что места в стеке недостаточно
            Console.WriteLine(insufficientExecutionStackEx.Message);
         }
         catch (StackOverflowException stackOverflowEx)
         {
            // NOTE: Это исключение совершенно бесполезно перехватывать
            Console.WriteLine(stackOverflowEx.Message);
         }
      }

      private static void ForseOverflow(bool forse)
      {
         if (forse)
         {
            RuntimeHelpers.EnsureSufficientExecutionStack();
            ForseOverflow(true);
         }
      }
   }
}