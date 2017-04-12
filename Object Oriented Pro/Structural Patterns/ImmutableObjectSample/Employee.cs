using System;
using System.Diagnostics.Contracts;

namespace ImmutableObjectSample
{
   public sealed class Employee : IEquatable<Employee>
   {
      private readonly string _firstName;
      private readonly int _id;
      private readonly string _lastName;

      public Employee(int id, string firstName, string lastName, double bonus, double salary)
      {
         _id = id;
         _firstName = firstName;
         _lastName = lastName;
         Bonus = bonus;
         Salary = salary;
      }

      public double Bonus { get; set; }
      public double Salary { get; set; }

      public bool Equals(Employee other)
      {
         return !ReferenceEquals(null, other) && (ReferenceEquals(this, other) ||
                                                  string.Equals(_firstName, other._firstName) && _id == other._id &&
                                                  string.Equals(_lastName, other._lastName));
      }      
      
      [Pure]
      public double RaiseSalary(double aPercent)
      {
         Salary += aPercent * Salary;
         return Salary;
      }

      public double CalculateSalary()
      {
         return Salary + Bonus;
      }

      public override string ToString()
      {
         return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", _id, _firstName, _lastName);
      }

      public override bool Equals(object obj)
      {
         return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) ||
                                                obj is Employee && Equals((Employee) obj));
      }

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = _firstName != null ? _firstName.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ _id;
            hashCode = (hashCode * 397) ^ (_lastName != null ? _lastName.GetHashCode() : 0);
            return hashCode;
         }
      }

      public static bool operator ==(Employee left, Employee right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(Employee left, Employee right)
      {
         return !Equals(left, right);
      }
   }
}