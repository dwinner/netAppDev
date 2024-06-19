namespace BuilderFacets;

public class PersonBuilder // facade
{
   // the object we're going to build
   protected readonly Person Person;

   public PersonBuilder() => Person = new Person();
   protected PersonBuilder(Person person) => Person = person;

   public PersonAddressBuilder Lives => new(Person);
   public PersonJobBuilder Works => new(Person);

   public static implicit operator Person(PersonBuilder personBuilder) => personBuilder.Person;
}