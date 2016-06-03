using System.Runtime.Serialization;
using CustomerInterface;
using RentalInterface;

namespace ExternalInterface
{
   [DataContract]
   public class RentalContract
   {
      [DataMember]
      public string Company { get; set; }

      [DataMember]
      public string CompanyReferenceId { get; set; }

      [DataMember]
      public RentalRegistration RentalRegistration { get; set; }

      [DataMember]
      public Customer Customer { get; set; }       
   }
}