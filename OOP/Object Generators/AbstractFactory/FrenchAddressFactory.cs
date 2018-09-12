namespace AbstractFactory
{
   internal sealed class FrenchAddressFactory : IAddressFactory
   {
      public Address CreateAddress() => new FrenchAddress();

      public PhoneNumber CreatePhoneNumber() => new FrenchPhoneNumber();
   }
}