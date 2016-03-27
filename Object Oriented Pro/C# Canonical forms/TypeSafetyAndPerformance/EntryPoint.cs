/**
 * Безопасность типов и производительность
 */

using System;

namespace TypeSafetyAndPerformance
{
   static class EntryPoint
   {
      static void Main()
      {
         var staff = new WorkForce();
         foreach (Employee employee in staff)
         {
            employee.Evaluate();
         }

         Console.ReadKey();
      }
   }
}
