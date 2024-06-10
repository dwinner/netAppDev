/**
 * Безопасность типов и производительность
 */

using System;

namespace TypeSafetyAndPerformance
{
   internal static class EntryPoint
   {
      private static void Main()
      {
         var staff = new WorkForce();
         foreach (var employee in staff)
         {
            employee.Evaluate();
         }

         Console.ReadKey();
      }
   }
}