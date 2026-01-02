using static System.Console;

WriteLine("Understanding Mutability and Immutability.");
Employee emp1 = new("Sam", 1);
WriteLine(emp1);
//emp1.Id = 2;// Error CS0272
var temp = emp1;
WriteLine($"The emp1's hashcode:{emp1.GetHashCode()}");
WriteLine($"The temp's hashcode:{temp.GetHashCode()}");

// To change Sam's ID, we create a new object
emp1 = new Employee("Sam", 2);
WriteLine(emp1);
WriteLine($"The emp1's hashcode:{emp1.GetHashCode()}");
WriteLine($"The temp's hashcode:{temp.GetHashCode()}");

internal class Employee
{
   public Employee(string name, int id)
   {
      Name = name;
      Id = id;
   }

   public string Name { get; }
   public int Id { get; }
   public override string ToString() => $"Name: {Name}, ID: {Id}";
}