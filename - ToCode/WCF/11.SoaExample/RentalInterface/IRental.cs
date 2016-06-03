using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace RentalInterface
{
    [ServiceContract]
    public interface IRental
    {
        [OperationContract]
        [FaultContract(typeof(RentalRegisterFault))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        string RegisterCarRental(RentalRegistration rentalRegistration);

        [OperationContract]
        [FaultContract(typeof(RentalRegisterFault))]
        void RegisterCarRentalAsPayed(string rentalID);

        [OperationContract]
        [FaultContract(typeof(RentalRegisterFault))]
        void StartCarRental(string rentalID, string locationID);

        [OperationContract]
        [FaultContract(typeof(RentalRegisterFault))]
        void StopCarRental(string rentalID, string locationID);

        [OperationContract]
        [FaultContract(typeof(RentalRegisterFault))]
        RentalRegistration GetRentalRegistration(string rentalID);



    }

    [DataContract]
    public class RentalRegistration
    {
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string CarID { get; set; }
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

    [DataContract]
    public enum PaymentStatusEnum
    {
        [EnumMember(Value = "PUV")]
        PayedUpFrontByVoucher,
        [EnumMember(Value = "PUC")]
        PayedUpFrontByCreditCard,
        [EnumMember(Value = "TPP")]
        ToBePayedAtPickUp,
        [EnumMember(Value = "INV")]
        ToBePayedByInvoice
    }

    [DataContract]
    public enum IncludedInsurance
    {
        [EnumMember]
        LiabilityInsurance = 1,
        [EnumMember]
        FireInsurance = 2,
        [EnumMember]
        TheftProtection = 4,
        [EnumMember]
        AllRiskInsurance = 1 + 2 + 4
    }


    [DataContract(Name = "RentalRegisterFault", Namespace = "FaultContracts/RentalRegisterFault")]
    public class RentalRegisterFault
    {
        [DataMember]
        public string FaultDescription { get; set; }
        [DataMember]
        public int FaultID { get; set; }
    }


}
