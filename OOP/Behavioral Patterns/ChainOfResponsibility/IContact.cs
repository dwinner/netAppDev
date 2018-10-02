namespace ChainOfResponsibility
{
   public interface IContact
   {
      string FirstName { get; }

      string LastName { get; }

      string Title { get; }

      string Organization { get; }
   }
}