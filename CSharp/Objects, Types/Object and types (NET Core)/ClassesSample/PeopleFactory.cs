public static class PeopleFactory
{
   public static int PersonCount { get; private set; }

   public static Person CreatePerson(string firstName, string lastName)
   {
      PersonCount++;
      return new Person(firstName, lastName);
   }
}