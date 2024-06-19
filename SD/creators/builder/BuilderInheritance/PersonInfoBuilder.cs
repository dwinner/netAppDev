namespace BuilderInheritance;

public class PersonInfoBuilder<TSelf> : PersonBuilder
   where TSelf : PersonInfoBuilder<TSelf>
{
   public TSelf Called(string name)
   {
      Person.Name = name;
      return (TSelf)this;
   }
}