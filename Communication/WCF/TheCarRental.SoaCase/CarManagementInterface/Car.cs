using System.Runtime.Serialization;

namespace CarManagementInterface
{
   [DataContract]
   public class Car
   {
      [DataMember]
      public string BrandName { get; set; }

      [DataMember]
      public string TypeName { get; set; }

      [DataMember]
      public TransmissionTypeEnum Transmission { get; set; }

      [DataMember]
      public int NumberOfDoors { get; set; }

      [DataMember]
      public int MaxNumberOfPersons { get; set; }

      [DataMember]
      public int LitersOfLuggage { get; set; }
   }
}