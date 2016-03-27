namespace Memento
{
   public interface IAddress
   {
      string Type { get; set; }

      string Description { get; set; }

      string Street { get; set; }

      string City { get; set; }

      string State { get; set; }

      string ZipCode { get; set; }
   }
}