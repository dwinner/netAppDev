using System;
using System.Runtime.Serialization;

namespace RentalInterface
{
   [Flags, DataContract]
   public enum IncludedInsurance
   {
      [EnumMember]
      None,

      [EnumMember]
      LiabilityInsurance = 0x1,

      [EnumMember]
      FireInsurance = 0x2,

      [EnumMember]
      TheftProtection = 0x4,

      [EnumMember]
      AllRiskInsurance = LiabilityInsurance | FireInsurance | TheftProtection
   }
}