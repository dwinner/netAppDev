using System.Runtime.Serialization;

namespace RentalInterface
{
   [DataContract]
   public enum PaymentStatusEnum
   {
      [EnumMember(Value = "PUV")]
      PaidUpFrontByVoucher,

      [EnumMember(Value = "PUC")]
      PaidUpFrontByCreditCard,

      [EnumMember(Value = "TPP")]
      ToBePaidAtPickUp,

      [EnumMember(Value = "INV")]
      ToBePaidByInvoice
   }
}