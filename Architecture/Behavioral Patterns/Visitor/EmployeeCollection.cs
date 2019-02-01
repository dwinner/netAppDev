using System.Collections;
using System.Collections.Generic;

namespace Visitor
{
   public class EmployeeCollection : IEnumerable<Employee>, IElement
   {
      private EmployeeCollection(IList<Employee> employees)
      {
         Employees = employees;
      }

      public EmployeeCollection()
         : this(new List<Employee>())
      {
      }

      public IList<Employee> Employees { get; }

      public void Accept(IVisitor visitor)
      {
         foreach (var employee in Employees)
            employee.Accept(visitor);
      }

      public IEnumerator<Employee> GetEnumerator() => Employees.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
   }
}