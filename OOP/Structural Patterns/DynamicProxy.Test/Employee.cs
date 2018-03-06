using System;

namespace TestConsole
{
   public class Employee : IEmployee
   {
      public Employee(Int32? employeeid, string firstname, string lastname, DateTime bDay)
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

      private Int32? EmployeeId { get; set; }

      private String FirstName { get; set; }

      private String LastName { get; set; }

      private DateTime DateOfBirth { get; set; }

      #endregion
   }
}