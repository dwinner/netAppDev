namespace Changing_state_over_time;

public class ChangeNameEvent : Event<Person>
{
   public ChangeNameEvent(string name)
   {
      NewName = name;
      EventName = nameof(ChangeNameEvent);
      EventDescription = "Changes a Persons name";
   }

   public string NewName { get; }

   public override Person ApplyEventTo(Person person)
   {
      var updated = new Person(person)
      {
         Name = NewName
      };
      updated.History.Add($"\nChanged name to {NewName}");
      return updated;
   }
}