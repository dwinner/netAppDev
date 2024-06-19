namespace PrototypeFactory;

public class Address(string streetAddress, string city, int suite)
{
   public Address(Address other) : this(other.StreetAddress, other.City, other.Suite)
   {
   }

   public string StreetAddress { get; } = streetAddress;

   public string City { get; } = city;

   public int Suite { get; set; } = suite;
}