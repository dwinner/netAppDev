using System.Collections.Generic;

namespace Proxy
{
   public interface IAddressBook<T> where T : IAddress
   {
      void Add(T anAddress);
      IList<T> AllAddresses { get; }
      T GetAddress(string description);
      void Open();
      void Save();
   }
}
