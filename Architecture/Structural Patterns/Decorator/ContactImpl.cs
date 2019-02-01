namespace Decorator
{
   public class ContactImpl : IContact
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Title { get; set; }

      public string Organization { get; set; }

      public ContactImpl() { }

      public ContactImpl(string firstName, string lastName, string title, string organization)
      {
         FirstName = firstName;
         LastName = lastName;
         Title = title;
         Organization = organization;
      }

      public override string ToString()
			=> $"FirstName: {FirstName}, LastName: {LastName}, Title: {Title}, Organization: {Organization}";
   }
}
