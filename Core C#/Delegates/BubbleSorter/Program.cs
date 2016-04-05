/**
 * Полезное применение делегатов: сортировка
 */

using System;

namespace BubbleSorter
{
   static class Program
   {
      static void Main()
      {
         Employee[] employees =
         {
            new Employee("Bugs Bunny", 20000),
            new Employee("Elmer Fudd", 10000),
            new Employee("Daffy Duck", 25000),
            new Employee("Wile Coyote", 1000000.38m),
            new Employee("Foghorn Leghorn", 23000),
            new Employee("RoadRunner", 50000)
         };

         // Сортировка по возрастанию
         BubbleSorter.Sort(employees, Employee.CompareSalary);
         Array.ForEach(employees, Console.WriteLine);

         Console.WriteLine();

         // Сортировка по убыванию
         BubbleSorter.Sort(employees, (first, second) => first.Salary < second.Salary, true);
         Array.ForEach(employees, Console.WriteLine);

         Console.ReadLine();
      }
   }
}
