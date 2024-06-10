using System;

namespace TestConsole
{
   public class Employee : IEmployee
   {
      public Employee(int? employeeid, string firstname, string lastname, DateTime bDay)
      {
         EmployeeId = employeeid;
         FirstName = firstname;
         LastName = lastname;
         DateOfBirth = bDay;
      }

      public void FullName()
      {
         Console.WriteLine("{0} {1}", FirstName, LastName);
      }

      public void Salary()
      {
         Console.WriteLine(10000.12f);
      }

      #region Properties

      private int? EmployeeId { get; set; }

      private string FirstName { get; }

      private string LastName { get; }

      private DateTime DateOfBirth { get; set; }

      #endregion
   }
}