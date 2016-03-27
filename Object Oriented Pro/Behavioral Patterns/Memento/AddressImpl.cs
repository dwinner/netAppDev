namespace Memento
{
   public class AddressImpl : IAddress
   {
      public string Type { get; set; }

      public string Description { get; set; }

      public string Street { get; set; }

      public string City { get; set; }

      public string State { get; set; }

      public string ZipCode { get; set; }

      public AddressImpl(string type, string description, string street, string city, string state, string zipCode)
      {
         Type = type;
         Description = description;
         Street = street;
         City = city;
         State = state;
         ZipCode = zipCode;
      }

      public AddressImpl()
      {         
      }

      public override string ToString()
      {
         return string.Format("Type: {0}, Description: {1}, Street: {2}, City: {3}, State: {4}, ZipCode: {5}", Type,
            Description, Street, City, State, ZipCode);
      }
   }
}