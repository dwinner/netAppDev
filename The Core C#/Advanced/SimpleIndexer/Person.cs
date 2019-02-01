namespace SimpleIndexer
{
   public class Person
   {
      public int Age { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public Person() { }

      public Person(string firstName, string lastName, int age)
      {
         Age = age;
         FirstName = firstName;
         LastName = lastName;
      }

      public override string ToString()
      {
         return string.Format("Name: {0} {1}, Age: {2}",
           FirstName, LastName, Age);
      }

      public class NullPerson : Person
      {
         public NullPerson(string firstName = "", string lastName = "", int age = -1)
            : base(firstName, lastName, age)
         { }
      }
   }
}
