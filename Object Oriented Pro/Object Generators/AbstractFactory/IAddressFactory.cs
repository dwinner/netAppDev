namespace AbstractFactory
{
   /// <summary>
   /// Интерфейс абстрактной фабрики
   /// </summary>
   public interface IAddressFactory
   {
      /// <summary>
      /// Фабричный метод создания адреса
      /// </summary>
      /// <returns>Адрес</returns>
      Address CreateAddress();

      /// <summary>
      /// Фабричный метод создания телефонного номера
      /// </summary>
      /// <returns>Телефонный номер</returns>
      PhoneNumber CreatePhoneNumber();
   }

   /// <summary>
   /// Семейство фабричных методов создания адресов Франции.
   /// </summary>
   public class FrenchAddressFactory : IAddressFactory
   {
      public Address CreateAddress()
      {
         return new FrenchAddress();
      }

      public PhoneNumber CreatePhoneNumber()
      {
         return new FrenchPhoneNumber();
      }
   }

   /// <summary>
   /// Семейство фабричных методов создания адресов США
   /// </summary>
   public class UsAddressFactory : IAddressFactory
   {
      public Address CreateAddress()
      {
         return new UsAddress();
      }

      public PhoneNumber CreatePhoneNumber()
      {
         return new UsPhoneNumber();
      }
   }
}
