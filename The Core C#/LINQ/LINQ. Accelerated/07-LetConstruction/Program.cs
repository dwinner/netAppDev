/**
 * Конструкция let
 */ 

using System;
using System.Collections.Generic;
using System.Linq;

namespace _07_LetConstruction
{
   class Program
   {
      static void Main()
      {
         var employees = new List<Employee>
         {
            new Employee { LastName = "Glasser", FirstName = "Ed" },
            new Employee { LastName = "Pupkin", FirstName = "Vasya" },
            new Employee { LastName = "Smails", FirstName = "Spaulding" },
            new Employee { LastName = "Ivanov", FirstName = "Ivan" }
         };
         var query = from emp in employees
                     let fullName = emp.FirstName + " " + emp.LastName
                     orderby fullName
                     select fullName;
         foreach (var item in query)
         {
            Console.WriteLine(item);
         }

         // Ссылки на другие коллекции

         var secondQuery = from x in Enumerable.Range(0, 10)
                           let innerRange = Enumerable.Range(0, 10)
                           from y in innerRange
                           select new { X = x, Y = y, Product = x * y };
         foreach (var item in secondQuery)
         {
            Console.WriteLine("{0} * {1} = {2}", item.X, item.Y, item.Product);
         }

         Console.ReadLine();
      }
   }

   public class Employee
   {
      public string LastName { get; set; }
      public string FirstName { get; set; }
   }
}
