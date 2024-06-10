using System.Collections.Generic;

namespace Proxy
{
   public interface IAddressBook<T> where T : IAddress
   {
      IList<T> AllAddresses { get; }
      void Add(T anAddress);
      T GetAddress(string description);
      void Open();
      void Save();
   }
}