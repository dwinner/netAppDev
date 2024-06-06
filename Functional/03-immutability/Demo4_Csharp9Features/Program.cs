using static System.Console;

WriteLine("Use of record type, init accessor, and with expression.");

// Original instance
Employee emp1 = new("Sam", 1);

//emp1.Id = 2;// // Error CS8852 now
// Creating a new instance keeping the same
// name, but modifying the ID
var emp2 = emp1 with { Id = 2 };


WriteLine($"Emp1: {emp1}");
WriteLine($"Emp2: {emp2}");

//WriteLine($"The emp1's hashcode:{emp1.GetHashCode()}");
//WriteLine($"The emp2's hashcode:{emp2.GetHashCode()}");

internal record Employee(string Name, int Id)
{
   public override string ToString() =>
      $"Name: {Name}, ID: {Id}";
}