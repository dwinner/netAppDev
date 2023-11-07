using System;

namespace Proxy
{
   [Serializable]
   public class AddressImpl : IAddress
   {
      public AddressImpl(
			string description, string street, string city,
			string state, string zipCode, string type)
      {
         Description = description;
         Street = street;
         City = city;
         State = state;
         ZipCode = zipCode;
         Type = type;
      }      

      public string Address { get; set; }
      public string Type { get; set; }
      public string Description { get; set; }
      public string Street { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public string ZipCode { get; set; }
   }
}