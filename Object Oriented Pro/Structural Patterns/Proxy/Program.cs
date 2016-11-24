/**
 * Суррогатный объект
 */

using static System.Console;

namespace Proxy
{
   internal static class Program
   {
      private static void Main()
      {
         var proxy = new AddressBookProxy("data.bin");
         proxy.Add(
            new AddressImpl(
               "Sun Education [CO]", "500 El Dorado Blvd.", "Broomfield", "CO", "80020", ""));
         proxy.Add(
            new AddressImpl(
               "Apple Inc.", "1 Infinite Loop", "Redwood City", "CA", "93741", ""));
         WriteLine(proxy.GetAddress("Sun Education [CO]").Address);
         var addresses = proxy.AllAddresses;
         WriteLine(addresses);
      }
   }
}