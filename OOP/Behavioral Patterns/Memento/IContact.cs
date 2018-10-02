namespace Memento
{
   public interface IContact
   {
      string FirstName { get; set; }

      string LastName { get; set; }

      string Title { get; set; }

      string Organization { get; set; }

      IAddress Address { get; set; }
   }
}