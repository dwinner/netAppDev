using System.Collections.Generic;
using System.Linq;

namespace Proxy
{
   public class AddressBookImpl : IAddressBook<IAddress>
   {
      private string _fileName;
      private readonly IList<IAddress> _addresses = new List<IAddress>();

      public AddressBookImpl(string fileName)
      {
         _fileName = fileName;
      }

      public void Add(IAddress anAddress)
      {
         if (!_addresses.Contains(anAddress))
         {
            _addresses.Add(anAddress);
         }
      }

      public IList<IAddress> AllAddresses => _addresses;

      public IAddress GetAddress(string description)
         => _addresses.FirstOrDefault(address => address.Description == description);

      public void Open() { /* Loading data... */ }

      public void Save() { /* Saving data... */ }
   }
}