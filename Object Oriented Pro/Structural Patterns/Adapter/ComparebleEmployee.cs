using System;

namespace Adapter
{
   public class ComparebleEmployee : IComparable<ComparebleEmployee>
   {
      private readonly IEmployee _employee;

      public ComparebleEmployee(IEmployee employee)
      {
         _employee = employee;
      }

      public int CompareTo(ComparebleEmployee other)
      {
         return _employee.Id.CompareTo(other._employee.Id);
      }

      public override string ToString()
      {
         return string.Format("{0}, {1}, {2}", _employee.Id, _employee.FirstName, _employee.LastName);
      }
   }
}
