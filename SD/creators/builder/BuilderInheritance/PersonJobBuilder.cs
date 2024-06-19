namespace BuilderInheritance;

public class PersonJobBuilder<TSelf>
   : PersonInfoBuilder<PersonJobBuilder<TSelf>>
   where TSelf : PersonJobBuilder<TSelf>
{
   public TSelf WorksAs(string position)
   {
      Person.Position = position;
      return (TSelf)this;
   }
}