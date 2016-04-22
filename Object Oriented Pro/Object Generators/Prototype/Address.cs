using System;

namespace Prototype
{
   [Serializable]
   public class Address : ICopy<Address>, ICloneable
   {
      #region Значения по умолчанию

      private const string DefaultType = "";

      private const string DefaultStreet = "";

      private const string DefaultCity = "";

      private const string DefaultState = "";

      private const string DefaultZipCode = "";

      #endregion


      #region Свойства

      public string Type { get; set; }

      public string Street { get; set; }

      public string City { get; set; }

      public string State { get; set; }

      public string ZipCode { get; set; }

      #endregion


      public Address(string type, string street, string city, string state, string zipCode)
      {
         Type = type;
         Street = street;
         City = city;
         State = state;
         ZipCode = zipCode;
      }

      public Address()
         : this(DefaultType, DefaultStreet, DefaultCity, DefaultState, DefaultZipCode)
      { }

      private Address(Address address)  // Копирующий конструктор
      {
         Type = address.Type;
         Street = address.Street;
         City = address.City;
         State = address.State;
         ZipCode = address.ZipCode;
      }

      public override string ToString()
      {
         return string.Format("Type: {0}, Street: {1}, City: {2}, State: {3}, ZipCode: {4}", Type, Street, City, State,
            ZipCode);
      }

      public Address Copy(bool deepCopy)
      {
         // Note: если не нужно исключение, то UniversalCopyUtility<Address>.DeepCopy(this)
         return deepCopy ? new DeepCopyManager<Address>(this).DeepCopy() : (Address)Clone();
      }

      public object Clone()
      {
         // Note: Если нет Copy ctor'а return MemberwiseClone();
         return new Address(this);
      }
   }
}
