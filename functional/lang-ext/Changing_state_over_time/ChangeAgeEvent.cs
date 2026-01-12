namespace Changing_state_over_time;

public class ChangeAgeEvent : Event<Person>
{
   public ChangeAgeEvent(int newAge)
   {
      NewAge = newAge;
      EventDescription = "\nContains information about changing a persons age";
      EventName = nameof(ChangeAgeEvent);
   }

   public int NewAge { get; }

   public override Person ApplyEventTo(Person person)
   {
      var updated = new Person(person)
      {
         Age = NewAge
      };
      updated.History.Add($"\nChanged age to {NewAge}");
      return updated;
   }
}