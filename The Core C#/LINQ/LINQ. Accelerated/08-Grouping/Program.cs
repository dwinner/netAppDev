/**
 * Группировка
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_Grouping
{
   class Program
   {
      static void Main()
      {
         // 1) Простой пример группировки результатов

         int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
         // Разделение чисел на четные и нечетные
         /*IEnumerable<IGrouping<int, int>>*/
         var query = from x in numbers
                     group x by x % 2;
         foreach (var group in query)
         {
            Console.WriteLine("mod2 == {0}", group.Key);
            foreach (var number in group)
            {
               Console.Write("{0}, ", number);
            }
            Console.WriteLine(Environment.NewLine);
         }

         // 2) Группировка по составным ключам

         var employees = new List<Employee>
         {
            new Employee { LastName = "Jones", FirstName = "Ed", Nationality = "American" },
            new Employee { LastName = "Ivanov", FirstName = "Vasya", Nationality = "Russian" },
            new Employee { LastName = "Jones", FirstName = "Tom", Nationality = "Welsh" },
            new Employee { LastName = "Smails", FirstName = "Spaulding", Nationality = "Irish" },
            new Employee { LastName = "Ivanov", FirstName = "Ivan", Nationality = "Russian" },
         };

         // Разобьем элементы по группам с той же национальностью и фамилией

         var groupQuery = from emp in employees
                          group emp by new { emp.Nationality, emp.LastName };
         foreach (var group in groupQuery)
         {
            Console.WriteLine(group.Key);
            foreach (var employee in group)
            {
               Console.WriteLine(employee.FirstName);
            }
            Console.WriteLine();
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
