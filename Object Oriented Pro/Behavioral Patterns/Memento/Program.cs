/**
 * Хранитель внутреннего состояния объекта
 */

using System;

namespace Memento
{
   static class Program
   {
      static void Main()
      {
         IAddressBook addressBook = new AddressBook();
         addressBook.Add(new ContactImpl("Peter", "Taggart", "Commander", "NSEA Protector", new AddressImpl()));
         addressBook.Add(new ContactImpl("Tawny", "Madison", "Lieutenant", "NSEA Protector", new AddressImpl()));
         addressBook.Add(new ContactImpl("Dr.", "Lazarus", "Dr.", "NSEA Protector", new AddressImpl()));
         addressBook.Add(new ContactImpl("Tech Sargent", "Chen", "Tech Sargent", "NSEA Protector", new AddressImpl()));
         Console.WriteLine(addressBook);
         object addrBookSnap = addressBook.Snapshot;  // Снимок объекта

         Console.WriteLine();

         // Предположим, что состояние объекта кардинальным образом изменилось
         addressBook.RemoveAll();
         addressBook.Add(new ContactImpl("Jason", "Nesmith", "", "Actor's Guild", new AddressImpl()));
         addressBook.Add(new ContactImpl("Gwen", "DeMarco", "", "Actor's Guild", new AddressImpl()));
         addressBook.Add(new ContactImpl("Alexander", "Dane", "", "Actor's Guild", new AddressImpl()));
         addressBook.Add(new ContactImpl("Fred", "Kwan", "", "Actor's Guild", new AddressImpl()));

         // Однако понадобилось восстановить прежнее состояние
         addressBook.Snapshot = addrBookSnap;
         Console.WriteLine(addressBook);
      }
   }
}
