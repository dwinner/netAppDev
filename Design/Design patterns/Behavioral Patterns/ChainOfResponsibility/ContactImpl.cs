namespace ChainOfResponsibility
{
   public class ContactImpl : IContact
   {
      public string FirstName { get; }

      public string LastName { get; }

      public string Title { get; }

      public string Organization { get; }

      public ContactImpl(string firstName, string lastName, string title, string organization)
      {
         FirstName = firstName;
         LastName = lastName;
         Title = title;
         Organization = organization;
      }

      public override string ToString() =>
         $"FirstName: {FirstName}, LastName: {LastName}, Title: {Title}, Organization: {Organization}";
   }
}