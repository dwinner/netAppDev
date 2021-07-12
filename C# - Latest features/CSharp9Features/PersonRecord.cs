namespace CSharp9Features
{
   public record PersonRecord
   {
      public PersonRecord()
      {
      }

      public PersonRecord(string firstName, string lastName)
      {
         FirstName = firstName;
         LastName = lastName;
      }

      public string FirstName { get; init; }

      public string LastName { get; init; }

      public string[] PhoneNumbers { get; init; }
   }
}