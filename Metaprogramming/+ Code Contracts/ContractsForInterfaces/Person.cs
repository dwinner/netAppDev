namespace ContractsForInterfaces
{
   public class Person : IPerson
   {
      public Person(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }

      public Person()
      {
      }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public int Age { get; set; }

      public void ChangeName(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }
   }
}