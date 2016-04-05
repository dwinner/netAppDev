/**
 * Сортировка результатов запроса
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_OrderByOperation
{
   class Program
   {
      static void Main()
      {
         var employees = new List<Employee>
         {
            new Employee { LastName = "Glasser", FirstName = "Ed", Nationality = "American" },
            new Employee { LastName = "Pupkin", FirstName = "Vasya", Nationality = "Russian" },
            new Employee { LastName = "Smails", FirstName = "Spaulding", Nationality = "Irish" },
            new Employee { LastName = "Ivanov", FirstName = "Ivan", Nationality = "Russian" }
         };

         var query = from emp in employees
                     orderby emp.Nationality,
                             emp.LastName descending,
                             emp.FirstName descending
                     select emp;
         foreach (var item in query)
         {
            Console.WriteLine("{0},\t{1},\t{2}", item.LastName, item.FirstName, item.Nationality);
         }

         Console.ReadLine();
      }
   }

   public class Employee
   {
      public string LastName { get; set; }
      public string FirstName { get; set; }
      public string Nationality { get; set; }
   }
}
