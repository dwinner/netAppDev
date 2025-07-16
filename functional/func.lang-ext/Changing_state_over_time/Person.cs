using System.Collections.Generic;

namespace Changing_state_over_time;

public class Person()
{
   public readonly List<string> History = [];

   public Person(Person person) : this()
   {
      Age = person.Age;
      History = person.History;
      Name = person.Name;
      Expertise = person.Expertise;
      Role = person.Role;
   }

   public int Age { get; set; }

   public string Name { get; set; } = "NoNameSet";

   public string Expertise { get; set; } = "NoExpertiseSet";

   public string Role { get; set; } = "NoRoleSet";

   public override string ToString()
      =>
         $"Person's name is {Name} and is {Age} years old now. Expertise is '{Expertise}', while Role is '{Role}'\nPrevious state changes were: {string.Join(',', History)}";
}