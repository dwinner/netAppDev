/**
 * Хранитель внутреннего состояния объекта
 */

using static System.Console;

namespace Memento
{
   internal static class Program
   {
      private static void Main()
      {
         IAddressBook addressBook = new AddressBook();
         addressBook.Add(
            new ContactImpl("Peter", "Taggart", "Commander", "NSEA Protector", new AddressImpl()));
         addressBook.Add(
            new ContactImpl("Tawny", "Madison", "Lieutenant", "NSEA Protector", new AddressImpl()));
         addressBook.Add(
            new ContactImpl("Dr.", "Lazarus", "Dr.", "NSEA Protector", new AddressImpl()));
         addressBook.Add(
            new ContactImpl("Tech Sargent", "Chen", "Tech Sargent", "NSEA Protector", new AddressImpl()));
         WriteLine(addressBook);
         var addrBookSnap = addressBook.Snapshot; // Object snapshot

         WriteLine();

         // Suppose, that the object state has radically changed
         addressBook.RemoveAll();
         addressBook.Add(
            new ContactImpl("Jason", "Nesmith", string.Empty, "Actor's Guild", new AddressImpl()));
         addressBook.Add(
            new ContactImpl("Gwen", "DeMarco", string.Empty, "Actor's Guild", new AddressImpl()));
         addressBook.Add(
            new ContactImpl("Alexander", "Dane", string.Empty, "Actor's Guild", new AddressImpl()));
         addressBook.Add
            (new ContactImpl("Fred", "Kwan", string.Empty, "Actor's Guild", new AddressImpl()));

         // However, it was necessary to restore the previous state
         addressBook.Snapshot = addrBookSnap;
         WriteLine(addressBook);
      }
   }
}