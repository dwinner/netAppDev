/*
 * Выражения запросов
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_QueryExpressions
{
   class EntryPoint
   {
      static void Main()
      {
         // Создать базу данных сотрудников
         var employees = new List<Employee>
         {
            new Employee { FirstName = "Joe", LastName = "Bob", Salary = 94000, StartDate = DateTime.Parse("1/4/1992") },
            new Employee { FirstName = "Jane", LastName = "Doe", Salary = 123000, StartDate = DateTime.Parse("4/12/1998") },
            new Employee { FirstName = "Milton", LastName = "Waddams", Salary = 1000000, StartDate = DateTime.Parse("12/3/1969") }
         };
         var query = from employee in employees
                     where employee.Salary > 100000
                     orderby employee.LastName, employee.FirstName
                     select new {employee.LastName, employee.FirstName };

         /* Альтернативный синтаксис расширяющих методов:
         var extQuery = employees.Where(emp => emp.Salary > 10000)
            .OrderBy(emp => emp.LastName)
            .OrderBy(emp => emp.FirstName)
            .Select(emp => new { LastName = emp.LastName, FirstName = emp.FirstName });*/

         Console.WriteLine("Высокооплачиваемые сотрудники:");
         foreach (var item in query)
         {
            Console.WriteLine("{0}, {1}",
               item.LastName,
               item.FirstName);
         }
         Console.ReadKey();
      }
   }

   public class Employee
   {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public decimal Salary { get; set; }
      public DateTime StartDate { get; set; }
   }
}
