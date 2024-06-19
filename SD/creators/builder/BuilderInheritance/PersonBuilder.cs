namespace BuilderInheritance;

public abstract class PersonBuilder
{
   protected readonly Person Person = new();

   public Person Build() => Person;
}