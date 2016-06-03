using System;
using System.Runtime.Serialization;

namespace RentalInterface
{
   [DataContract]
   public class RentalRegistration
   {
      [DataMember]
      public int CustomerId { get; set; }

      [DataMember]
      public string CarId { get; set; }

      [DataMember]
      public int PickUpLocation { get; set; }

      [DataMember]
      public int DropOffLocation { get; set; }

      [DataMember]
      public DateTime PickUpDateTime { get; set; }

      [DataMember]
      public DateTime DropOffDateTime { get; set; }

      [DataMember]
      public PaymentStatusEnum PaymentStatus { get; set; }

      [DataMember]
      public string Comments { get; set; }
   }
}