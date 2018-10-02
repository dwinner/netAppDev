using System;

namespace Adapter
{
   public sealed class ComparebleEmployee : IComparable<ComparebleEmployee>
   {
      private readonly IEmployee _employee;

      public ComparebleEmployee(IEmployee employee)
      {
         _employee = employee;
      }

      public int CompareTo(ComparebleEmployee other)
         => _employee.Id.CompareTo(other._employee.Id);

      public override string ToString()
         => $"{_employee.Id}, {_employee.FirstName}, {_employee.LastName}";
   }
}