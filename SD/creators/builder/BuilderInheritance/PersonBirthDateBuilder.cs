namespace BuilderInheritance;

// here's another inheritance level
// note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

public class PersonBirthDateBuilder<TSelf>
   : PersonJobBuilder<PersonBirthDateBuilder<TSelf>>
   where TSelf : PersonBirthDateBuilder<TSelf>
{
   public TSelf Born(DateTime dateOfBirth)
   {
      Person.DateOfBirth = dateOfBirth;
      return (TSelf)this;
   }
}