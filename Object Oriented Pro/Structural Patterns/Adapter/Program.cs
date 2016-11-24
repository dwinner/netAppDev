/**
 * Object adapter.
 */

using System;
using System.Collections.Generic;

namespace Adapter
{
   internal static class Program
   {
      private static void Main()
      {
         IEmployee firstEmployee = new EmployeeImpl(1, "Alex", "Davis");
         IEmployee secondEmployee = new EmployeeImpl(2, "Cristian", "Nagel");
         IEmployee thirdEmployee = new EmployeeImpl(3, "Ben", "Watson");

         // 1) Sorting via the object adapter
         var employeeArray = new[]
         {
            new ComparebleEmployee(secondEmployee),
            new ComparebleEmployee(firstEmployee),
            new ComparebleEmployee(thirdEmployee)
         };
         PrintArray(employeeArray);
         Array.Sort(employeeArray);
         PrintArray(employeeArray);

         // 2) Sorting via the callback strategy
         var empArray = new[] { thirdEmployee, secondEmployee, firstEmployee };
         PrintArray(empArray);
         Array.Sort(empArray,
            (aFirstEmp, aSecondEmp) => aFirstEmp.Id.CompareTo(aSecondEmp.Id));
         PrintArray(empArray);

         Console.ReadKey();
      }

      private static void PrintArray<T>(IEnumerable<T> employeeArray)
      {
         foreach (var comparable in employeeArray)
         {
            Console.WriteLine(comparable);
         }
      }
   }
}