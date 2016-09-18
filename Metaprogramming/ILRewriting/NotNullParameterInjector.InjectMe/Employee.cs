using System;
using NotNullAttribute.Lib;

namespace NotNullParameterInjector.InjectMe
{
   public class Employee
   {
      public string Name { get; set; }

      public string Salary { get; set; }

      public void Hire([NotNullRequired] Employee employee)
      {
         Console.WriteLine($"{employee.Name} with {employee.Salary} hired");
      }
   }
}