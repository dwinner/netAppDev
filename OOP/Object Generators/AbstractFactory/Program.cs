using System;

namespace AbstractFactory
{
   internal static class Program
   {
      private static void Main()
      {
         // Creating USA-addresses.
         IAddressFactory usAddressFactory = new UsAddressFactory();
         var usAddress = usAddressFactory.CreateAddress();
         var usPhoneNumber = usAddressFactory.CreatePhoneNumber();
         usAddress.Street = "142 Lois Lane";
         usAddress.City = "Metropolis";
         usAddress.Region = "WY";
         usAddress.PostalCode = "54321";
         usPhoneNumber.TelephoneNumber = "7039214722";

         // Creating France-addresses.
         IAddressFactory frenchAddressFactory = new FrenchAddressFactory();
         var frenchAddress = frenchAddressFactory.CreateAddress();
         var frenchPhoneNumber = frenchAddressFactory.CreatePhoneNumber();
         frenchAddress.Street = "21 Rue Victor Hugo";
         frenchAddress.City = "Courbevoie";
         frenchAddress.Region = "Fr";
         frenchAddress.PostalCode = "40792";
         frenchPhoneNumber.TelephoneNumber = "011324290";

         Console.ReadKey();
      }
   }
}