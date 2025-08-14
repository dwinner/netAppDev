namespace Changing_state_over_time;

public class ChangeExpertiseEvent(string newExpertise) : Event<Person>
{
   public string NewExpertise { get; } = newExpertise;

   public override Person ApplyEventTo(Person person)
   {
      var updated = new Person(person)
      {
         Expertise = NewExpertise
      };
      updated.History.Add($"\nChanged Expertise to {NewExpertise}");
      return updated;
   }
}