using System;

namespace EmployeeAppWithPartial
{
   internal partial class Employee
   {
      #region Constructors

      public Employee()
      {
      }

      public Employee(string name, int id, float pay)
         : this(name, 0, id, pay, "")
      {
      }

      public Employee(string name, int age, int id, float pay, string ssn)
      {
         // Better!  Use properties when setting class data.
         // This reduces the amount of duplicate error checks.
         Name = name;
         Age = age;
         ID = id;
         Pay = pay;
         SocialSecurityNumber = ssn;
      }

      static Employee() => Company = "Intertech Training";

      #endregion

      #region Properties

      public static string Company { get; set; }

      public string Name
      {
         get => empName;
         set
         {
            // Here, value is really a string.
            if (value.Length > 15)
            {
               Console.WriteLine("Error!  Name must be less than 15 characters!");
            }
            else
            {
               empName = value;
            }
         }
      }

      public int ID { get; set; }

      public float Pay { get; set; }

      public int Age { get; set; }

      public string SocialSecurityNumber { get; set; }

      #endregion
   }
}