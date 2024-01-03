namespace Proxy
{
   public interface IAddress
   {
      string Address { get; set; }
      string Type { get; set; }
      string Description { get; set; }
      string Street { get; set; }
      string City { get; set; }
      string State { get; set; }
      string ZipCode { get; set; }
   }
}
