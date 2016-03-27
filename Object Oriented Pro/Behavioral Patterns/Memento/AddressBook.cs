using System;
using System.Collections.Generic;
using System.Text;

namespace Memento
{
   public class AddressBook : IAddressBook
   {
      private ISet<IContact> _contacts = new HashSet<IContact>();

      public IEnumerable<IContact> Contacts { get { return _contacts; } }

      public object Snapshot
      {
         get
         {
            return new AddressBookMemento(new HashSet<IContact>(Contacts));
         }
         set
         {
            var bookMemento = value as AddressBookMemento;
            if (bookMemento != null)
            {
               _contacts = bookMemento.ContactsSnapshot;
            }
         }
      }

      public bool Add(IContact contact)
      {
         return _contacts.Add(contact);
      }

      public bool Remove(IContact contact)
      {
         return _contacts.Remove(contact);
      }

      public void RemoveAll()
      {
         _contacts.Clear();
      }

      public override string ToString()
      {
         var stringBuilder = new StringBuilder(_contacts.Count * 0x40);
         foreach (var contact in Contacts)
         {
            stringBuilder.Append(contact + Environment.NewLine);
         }

         return stringBuilder.ToString();
      }

      private class AddressBookMemento
      {
         public ISet<IContact> ContactsSnapshot
         {
            get { return _contacts; }
         }
         private readonly ISet<IContact> _contacts;

         public AddressBookMemento(ISet<IContact> contacts)
         {
            _contacts = contacts;
         }
      }
   }
}