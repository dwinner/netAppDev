using System;
using System.Runtime.Serialization;

namespace CustomerInterface
{
   [DataContract]
   public class Customer
   {
      [DataMember]
      public string Name { get; set; }

      [DataMember]
      public string FirstName { get; set; }

      [DataMember]
      public string MiddleLeter { get; set; }

      [DataMember]
      public DateTime BirthDate { get; set; }
   }
}