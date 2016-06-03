using System.Runtime.Serialization;

namespace CarManagementInterface
{
   [DataContract]
   public enum TransmissionTypeEnum
   {
      [EnumMember]
      Manual,

      [EnumMember]
      Automatic
   }
}