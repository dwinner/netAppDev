using System;

namespace AbstractFactory
{
   class Program
   {
      static void Main()
      {
         // Создание адресов США.
         IAddressFactory usAddressFactory = new UsAddressFactory();
         Address usAddress = usAddressFactory.CreateAddress();
         PhoneNumber usPhoneNumber = usAddressFactory.CreatePhoneNumber();
         usAddress.Street = "142 Lois Lane";
         usAddress.City = "Metropolis";
         usAddress.Region = "WY";
         usAddress.PostalCode = "54321";
         usPhoneNumber.TelephoneNumber = "7039214722";

         // Создание адресов Франции.
         IAddressFactory frenchAddressFactory = new FrenchAddressFactory();
         Address frenchAddress = frenchAddressFactory.CreateAddress();
         PhoneNumber frenchPhoneNumber = frenchAddressFactory.CreatePhoneNumber();
         frenchAddress.Street = "21 Rue Victor Hugo";
         frenchAddress.City = "Courbevoie";
         frenchAddress.Region = "Fr";
         frenchAddress.PostalCode = "40792";
         frenchPhoneNumber.TelephoneNumber = "011324290";

         Console.ReadKey();
      }
   }
}
