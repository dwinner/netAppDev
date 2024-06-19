namespace CopyConstructor;

public class Address
{
   public Address(string streetAddress, string city, string country)
   {
      StreetAddress = streetAddress ?? throw new ArgumentNullException(nameof(streetAddress));
      City = city ?? throw new ArgumentNullException(nameof(city));
      Country = country ?? throw new ArgumentNullException(nameof(country));
   }

   public Address(Address other)
   {
      StreetAddress = other.StreetAddress;
      City = other.City;
      Country = other.Country;
   }

   public string StreetAddress { get; }

   public string City { get; }

   public string Country { get; }

   public override string ToString() =>
      $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
}