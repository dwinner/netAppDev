using System;
using System.Collections.Generic;
using System.Linq;

namespace Proxy
{   
   public class AddressBookProxy : IAddressBook<IAddress>
   {
      private readonly string _file;
      private readonly IList<IAddress> _localAddresses = new List<IAddress>();
      private IAddressBook<IAddress> _addressBook;

      public AddressBookProxy(string aFileName)
      {
         _file = aFileName;
      }

      public void Add(IAddress anAddress)
      {
         if (_addressBook != null)
         {
            _addressBook.Add(anAddress);
         }
         else if (!_localAddresses.Contains(anAddress))
         {
            _localAddresses.Add(anAddress);
         }
      }

      public IList<IAddress> AllAddresses
      {
         get
         {
            if (_addressBook != null)
            {
               return _addressBook.AllAddresses;
            }

            Open();
            return _addressBook != null
               ? _addressBook.AllAddresses
               : new List<IAddress>(0);
         }
      }

      public IAddress GetAddress(string description)
      {
         if (_localAddresses.Count > 0)
         {
            foreach (var address
					in _localAddresses.Where(address => address.Description == description))
            {
               return address;
            }
         }

         if (_addressBook == null)
         {
            Open();
         }

         if (_addressBook != null)
         {
            return _addressBook.GetAddress(description);
         }

         throw new InvalidOperationException();
      }

      public void Open()
      {
         if (_addressBook == null)
         {
            _addressBook = new AddressBookImpl(_file);
         }

         foreach (var address in _localAddresses)
         {
            _addressBook.Add(address);
         }
      }

      public void Save()
      {
         if (_addressBook != null)
         {
            _addressBook.Save();
         }
         else if (_localAddresses.Count > 0)
         {
            Open();
            _addressBook?.Save();
         }
      }
   }
}
