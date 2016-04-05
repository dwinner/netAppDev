/**
 * Делегат открытого экземпляра
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace _04_OpenInstanceDelegate
{
   class Program
   {
      static void Main(string[] args)
      {
         List<Employee> employees = new List<Employee>();
         employees.Add(new Employee(40000));
         employees.Add(new Employee(65000));
         employees.Add(new Employee(95000));

         // Создать делегат открытого экземпляра
         MethodInfo mi = typeof(Employee).GetMethod("ApplyRaiseOf", BindingFlags.Public | BindingFlags.Instance);
         ApplyRaiseDelegate<Employee> applyRaise =
            (ApplyRaiseDelegate<Employee>)
               Delegate.CreateDelegate(typeof(ApplyRaiseDelegate<Employee>), mi);

         // Выполнить повышение
         foreach (Employee e in employees)
         {
            applyRaise(e, (decimal)0.01);
            Console.WriteLine(e.Salary);  // Вывести значение новой зарплаты на консоль
         }

         Console.ReadKey();
      }
   }

   delegate void ApplyRaiseDelegate<T>(T employee, decimal percent);

   public class Employee
   {
      private decimal salary;

      public Employee(decimal salary)
      {
         this.salary = salary;
      }

      public decimal Salary
      {
         get { return salary; }
      }

      public void ApplyRaiseOf(decimal percent)
      {
         salary *= (1 + percent);
      }
   }
}
