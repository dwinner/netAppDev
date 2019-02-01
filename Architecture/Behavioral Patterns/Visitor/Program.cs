/**
 * Посетитель объектов
 */

using System;

namespace Visitor
{
   internal static class Program
   {
      private static void Main()
      {
         var employeeCollection = new EmployeeCollection();
         employeeCollection.Employees.Add(new Employee("Hank", 25000.0, 14));
         employeeCollection.Employees.Add(new Employee("Elly", 35000.0, 16));
         employeeCollection.Employees.Add(new Employee("Dick", 45000.0, 21));

         employeeCollection.Accept(new IncomeVisitor());
         employeeCollection.Accept(new VacationVisitor());

         Console.ReadLine();
      }
   }
}