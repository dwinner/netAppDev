using System;

namespace ImmutableObjectSample
{
   internal static class Program
   {
      private static void Main()
      {
         var employee = new Employee(1, "Bob", "Bob", default(double), 3000);
         Console.WriteLine(employee);

         Console.WriteLine("Bonus: {0}", employee.Bonus);
         Console.WriteLine("Salary: {0}", employee.Salary);

         Console.WriteLine("Calculated salary: {0}", employee.CalculateSalary());
         employee.RaiseSalary(.3);
         Console.WriteLine("Calculated salary: {0}", employee.CalculateSalary());
      }
   }
}