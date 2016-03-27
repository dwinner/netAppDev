using System;
using System.Collections;
using System.Collections.Generic;

namespace Visitor
{
   public class EmployeeCollection : IEnumerable<Employee>, IElement
   {
      private readonly IList<Employee> _employees;

      public IList<Employee> Employees
      {
         get { return _employees; }
      }

      public EmployeeCollection(IList<Employee> employees)
      {
         _employees = employees;
      }

      public EmployeeCollection()
         : this(new List<Employee>()) { }

      public IEnumerator<Employee> GetEnumerator()
      {
         return _employees.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public void Accept(IVisitor visitor)
      {
         foreach (var employee in _employees)
         {
            employee.Accept(visitor);
         }
         Console.WriteLine();
      }
   }
}