using System.Collections.Generic;

namespace Memento
{
   public interface IAddressBook
   {
      IEnumerable<IContact> Contacts { get; }

      object Snapshot { get; set; }

      bool Add(IContact contact);

      bool Remove(IContact contact);

      void RemoveAll();
   }
}