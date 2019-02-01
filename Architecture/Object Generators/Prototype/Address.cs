using System;
using System.Threading;

namespace Prototype
{
   [Serializable]
   public sealed class Address : ICopy<Address>, ICloneable
   {
      private readonly Lazy<DeepCopyManager<Address>> _deferredAddrCopyManager =
         new Lazy<DeepCopyManager<Address>>(()
            => new DeepCopyManager<Address>(), LazyThreadSafetyMode.PublicationOnly);

      #region Constructors and cloning

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
      {
      }

      private Address(Address address) // Copy ctor
      {
         Type = address.Type;
         Street = address.Street;
         City = address.City;
         State = address.State;
         ZipCode = address.ZipCode;
      }

      object ICloneable.Clone() => new Address(this) /*MemberwiseClone()*/;

      public Address Copy(bool deepCopy = false)
         => deepCopy
            ? _deferredAddrCopyManager.Value.DeepCopy(this)
            : (Address) ((ICloneable) this).Clone();

      #endregion

      public override string ToString()
         => $"Type: {Type}, Street: {Street}, City: {City}, State: {State}, ZipCode: {ZipCode}";

      #region Defaults

      private const string DefaultType = ""; // Not L10N

      private const string DefaultStreet = ""; // Not L10N

      private const string DefaultCity = ""; // Not L10N

      private const string DefaultState = ""; // Not L10N

      private const string DefaultZipCode = ""; // Not L10N

      #endregion

      #region Properties

      public string Type { get; }

      public string Street { get; }

      public string City { get; }

      public string State { get; }

      public string ZipCode { get; }

      #endregion
   }
}