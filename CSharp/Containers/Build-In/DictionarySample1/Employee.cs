using System;

namespace DictionarySample
{
   [Serializable]
   public class Employee
   {
      private readonly string _name;
      private readonly decimal _salary;
      private readonly EmployeeId _id;

      public Employee(EmployeeId id, string name, decimal salary)
      {
         _id = id;
         _name = name;
         _salary = salary;
      }

      public override string ToString()
      {
         return string.Format("{0}: {1,-20} {2:C}", _id, _name, _salary);
      }
   }
}