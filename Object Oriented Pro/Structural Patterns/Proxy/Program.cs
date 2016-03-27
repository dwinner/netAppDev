/**
 * Суррогатный объект
 */ 

using System;
using System.Collections.Generic;

namespace Proxy
{
   internal static class Program
   {
      static void Main()
      {
         var proxy = new AddressBookProxy("data.bin");
         proxy.Add(
            new AddressImpl(
               "Sun Education [CO]", "500 El Dorado Blvd.", "Broomfield", "CO", "80020", ""));
         proxy.Add(
            new AddressImpl(
               "Apple Inc.", "1 Infinite Loop", "Redwood City", "CA", "93741", ""));
         Console.WriteLine(proxy.GetAddress("Sun Education [CO]").Address);
         IList<IAddress> addresses = proxy.AllAddresses;
         Console.WriteLine(addresses);
      }
   }
}
