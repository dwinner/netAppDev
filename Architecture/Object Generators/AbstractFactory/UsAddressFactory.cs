namespace AbstractFactory
{
   public sealed class UsAddressFactory : IAddressFactory
   {
      public Address CreateAddress() => new UsAddress();

      public PhoneNumber CreatePhoneNumber() => new UsPhoneNumber();
   }
}