using System.Collections.Generic;
using System.Linq;

namespace Proxy
{
   public class AddressBookImpl : IAddressBook<IAddress>
   {
      private string _fileName;

      public AddressBookImpl(string fileName) => _fileName = fileName;

      public void Add(IAddress anAddress)
      {
         if (!AllAddresses.Contains(anAddress))
         {
            AllAddresses.Add(anAddress);
         }
      }

      public IList<IAddress> AllAddresses { get; } = new List<IAddress>();

      public IAddress GetAddress(string description)
         => AllAddresses.FirstOrDefault(address => address.Description == description);

      public void Open()
      { /* Loading data... */
      }

      public void Save()
      { /* Saving data... */
      }
   }
}