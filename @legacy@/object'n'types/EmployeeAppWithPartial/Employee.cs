﻿using System;

namespace EmployeeAppWithPartial
{
   internal partial class Employee
   {
      // Field data.
      private string empName;

      #region Methods

      public void GiveBonus(float amount)
      {
         Pay += amount;
      }

      public void DisplayStats()
      {
         Console.WriteLine("Name: {0}", Name);
         Console.WriteLine("ID: {0}", ID);
         Console.WriteLine("Age: {0}", Age);
         Console.WriteLine("Pay: {0}", Pay);
         Console.WriteLine("SSN: {0}", SocialSecurityNumber);
      }

      // This would be a compiler error if you uncomment,
      // as the SocialSecurityNumber property resolves to 
      // these methods internally!
      //public string get_SocialSecurityNumber()
      //{
      //    return empSSN;
      //}
      //public void set_SocialSecurityNumber(string ssn)
      //{
      //    empSSN = ssn;
      //}

      #endregion
   }
}