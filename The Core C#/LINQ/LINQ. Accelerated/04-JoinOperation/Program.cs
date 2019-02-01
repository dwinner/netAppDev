/**
 * Объединение наборов в LINQ
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_JoinOperation
{
   class Program
   {
      static void Main()
      {
         // Построить коллекцию сотрудников
         var employees = new List<EmployeeId>
         {
            new EmployeeId { Id = "111-11-1111", Name = "Ed Classer" },
            new EmployeeId { Id = "222-22-2222", Name = "Spaulding Smails" },
            new EmployeeId { Id = "333-33-3333", Name = "Ivan Ivanov" },
            new EmployeeId { Id = "444-44-4444", Name = "Vasya Pupkin" }
         };

         // Построить коллекцию национальностей
         var empNationalities = new List<EmployeeNationality>
         {
            new EmployeeNationality { Id = "111-11-1111", Nationality = "American" },
            new EmployeeNationality { Id = "222-22-2222", Nationality = "Irish" },
            new EmployeeNationality { Id = "333-33-3333", Nationality = "Russian" },
            new EmployeeNationality { Id = "444-44-4444", Nationality = "Russian" }
         };

         // Построить запрос
         var query = from emp in employees
                     join n in empNationalities
                        on emp.Id equals n.Id
                     orderby n.Nationality descending
                     select new {emp.Id, emp.Name, n.Nationality };
         foreach (var person in query)
         {
            Console.WriteLine("{0}, {1}, \t{2}", person.Id, person.Name, person.Nationality);
         }

         Console.ReadKey();
      }
   }

   public class EmployeeId
   {
      public string Id { get; set; }
      public string Name { get; set; }
   }

   public class EmployeeNationality
   {
      public string Id { get; set; }
      public string Nationality { get; set; }
   }
}
