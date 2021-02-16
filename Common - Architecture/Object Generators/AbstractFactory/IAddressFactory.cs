namespace AbstractFactory
{   
   public interface IAddressFactory
   {      
      Address CreateAddress();
      
      PhoneNumber CreatePhoneNumber();
   }
}
