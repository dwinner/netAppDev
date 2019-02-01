/**
 * Инициализаторы коллекций
 */

using System;
using System.Collections.Generic;

namespace _16_CollectionInitializer
{
    class Program
    {
        static void Main()
        {
            var developmentTeam = new List<Employee>
                {
                    new Employee {Name = "Michael Bolton"},
                    new Employee {Name = "Samir Nagheenanajar"},
                    new Employee {Name = "Peter Gibbons"}
                };
            Console.WriteLine("Development team:");
            foreach (Employee employee in developmentTeam)
            {
                Console.WriteLine("\t" + employee.Name);
            }

            Console.ReadKey();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
    }
}
