using static System.Console;

WriteLine("Improving the shallow immutability.");
//Employee emp1 = new("Sam", 1);
//Employee emp2 = new("Bob", 2);
List<Employee> mathEmployees = new()
{
   new Employee("Sam", 1),
   new Employee("Bob", 2)
};

Department mathDept = new("Mathematics", mathEmployees);
WriteLine($"Employee count:{mathDept.Employees.Count}");

// We cannot mutate the state directly
// mathDept.Name = "Physics";// Error CS0200
// mathDept.Employees = null; //  Error CS0200

// Now the following line will raise a compile-time error
//mathDept.Employees.Add(new("Jack", 3)); // Error 1061 now

// Still, the following line shows no compile-time error
mathEmployees.Add(new Employee("Kate", 4));
WriteLine($"Employee count:{mathDept.Employees.Count}");

internal class Employee
{
   public Employee(string name, int id)
   {
      Name = name;
      Id = id;
   }

   public string Name { get; }
   public int Id { get; }

   public override string ToString() =>
      $"Name: {Name}, ID: {Id}";
}

internal class Department
{
   public Department(string name, IReadOnlyCollection<Employee> emps)
   {
      Name = name;
      Employees = emps;
   }

   public string Name { get; }

   //public List<Employee> Employees { get; }
   public IReadOnlyCollection<Employee> Employees { get; }
}