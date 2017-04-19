using System;
using System.Collections.Generic;
using System.Text;

namespace Memento
{
   public class AddressBook : IAddressBook
   {
      private ISet<IContact> _contacts = new HashSet<IContact>();

      public IEnumerable<IContact> Contacts => _contacts;

      public object Snapshot
      {
         get { return new AddressBookMemento(new HashSet<IContact>(Contacts)); }
         set
         {
            var bookMemento = value as AddressBookMemento;
            if (bookMemento != null)
               _contacts = bookMemento.ContactsSnapshot;
         }
      }

      public bool Add(IContact contact) => _contacts.Add(contact);

      public bool Remove(IContact contact) => _contacts.Remove(contact);

      public void RemoveAll() => _contacts.Clear();

      public override string ToString()
      {
         var stringBuilder = new StringBuilder(_contacts.Count * 0x40);
         foreach (var contact in Contacts)
            stringBuilder.AppendFormat("{0}{1}", contact, Environment.NewLine);

         return stringBuilder.ToString();
      }

      private class AddressBookMemento
      {
         public AddressBookMemento(ISet<IContact> contacts)
         {
            ContactsSnapshot = contacts;
         }

         public ISet<IContact> ContactsSnapshot { get; }
      }
   }
}