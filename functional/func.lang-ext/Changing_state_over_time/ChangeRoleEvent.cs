namespace Changing_state_over_time;

public class ChangeRoleEvent(string newRole) : Event<Person>
{
   public string NewRole { get; } = newRole;

   public override Person ApplyEventTo(Person person)
   {
      var updated = new Person(person)
      {
         Role = NewRole
      };
      updated.History.Add($"\nChanged Role to {NewRole}");
      return updated;
   }
}