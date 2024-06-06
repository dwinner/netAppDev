using static System.Console;

WriteLine("Exploring the popsicle immutability.");
Employee emp = new("Sam", 1);
WriteLine($"Employee detail: {emp}");
emp.Id = 2; // OK
WriteLine($"Employee detail: {emp}");
emp.Id = 3; // No Change
WriteLine($"Employee detail: {emp}");
emp.Id = 4; // No Change
WriteLine($"Employee detail: {emp}");

internal class Employee
{
   private static int _attemptToIdChanged = 1;
   private int _id;

   public Employee(string name, int id)
   {
      Name = name;
      Id = id;
   }

   public string Name { get; }

   public int Id
   {
      get => _id;
      set
      {
         if (_attemptToIdChanged < 3)
         {
            _id = value;
            WriteLine("The employee's ID is created.");
         }
         else
         {
            //WriteLine($"""
            //    The ID cannot be changed. (Maximum limit is reached).
            //    You have attempted to change the ID {AttemptToIdChanged} times.                    
            //    """);
            WriteLine($"""
                       The ID cannot be changed.
                       (Maximum limit is reached.)
                       You tried {_attemptToIdChanged} times.                    
                       """);
         }

         _attemptToIdChanged++;
      }
   }

   public override string ToString() =>
      $"Name: {Name}, ID: {Id}\n";
}