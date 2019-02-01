namespace BubbleSorter
{
   public class Employee
   {
      public string Name { get; private set; }

      public decimal Salary { get; private set; }

      public Employee(string name, decimal salary)
      {
         Name = name;
         Salary = salary;
      }

      public override string ToString()
      {
         return string.Format("{0}, {1:C}", Name, Salary);
      }

      public static bool CompareSalary(Employee firstEmployee, Employee secondEmployee)
      {
         return firstEmployee.Salary < secondEmployee.Salary;
      }
   }
}