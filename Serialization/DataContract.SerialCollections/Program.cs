/**
 * Serializing collection via DataContract attributes
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DataContract.SerialCollections
{
   internal static class Program
   {
      private static void Main()
      {
      }
   }

   [DataContract]
   [KnownType(typeof(UsAddress))]
   public class Address
   {
      [DataMember]
      public string Street { get; set; }

      [DataMember]
      public string Postcode { get; set; }
   }

   public class UsAddress : Address
   {
   }

   [CollectionDataContract(ItemName = "residence")]
   public class AddressList : Collection<Address>
   {
   }

   [CollectionDataContract(ItemName = "entry", KeyName = "kind", ValueName = "number")]
   public class PhoneNumberMap : Dictionary<string, string>
   {
   }

   [DataContract]
   public class Person
   {
      [DataMember]
      public PhoneNumberMap PhoneMap { get; set; }

      [DataMember]
      public AddressList Addresses { get; set; }
   }
}